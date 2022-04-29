using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Creature
{
	public class CreatureEntity : ISimulated
	{
		public Gene Gene { get; private set; }
		public IBehavior? CurrentBehavior { get; private set; }
		public Tile CurrentTile { get; set; }
		public CreatureStats Stats { get; private set; }

		private static double HungerMax = 1;
		private static double ThirstMax = 1;
		private static double ReproduceNeedMax = 1;
		private static int BehaviorAttentionSpan = 2;
		private static double HealthDamageThirst = 0.15;
		private static double HealthDamageHunger = 0.10;
		private static double HealthDamageReproduce = 0.05;

		private readonly SimulationModel _model;

		private double _health;
		private double _hunger = HungerMax;
		private double _thirst = ThirstMax;
		private double _reproduceNeed = ReproduceNeedMax;
		private int _stepsCurrentTask = 0;
		private int _age = 0;

		public CreatureEntity(SimulationModel model, Gene gene, Tile tile)
		{
			Gene = gene;
			CurrentTile = tile;
			_model = model;

			Stats = gene.GetStats();

			_health = Stats.HealthMax;
		}

		public bool ThirstSatisfied() => _thirst > ThirstMax / 3;
		public bool HungerSatisfied() => _hunger > HungerMax / 4;
		public bool ReproduceSatisfied() => _reproduceNeed > ReproduceNeedMax / 5;
		public void SatisfyThirst(double amount) => _thirst = Math.Clamp(_thirst + amount, 0, ThirstMax);
		public void SatisfyHunger(double amount) => _hunger = Math.Clamp(_hunger + amount, 0, HungerMax);
		public void SatisfyReproduce(double amount) => _reproduceNeed = Math.Clamp(_reproduceNeed + amount, 0, ReproduceNeedMax);
		public double ThirstValue() => ThirstMax - _thirst;
		public double HungerValue() => HungerMax - _hunger;
		public double ReproduceValue() => ReproduceNeedMax - _reproduceNeed;
		public bool DealDamage(double amount)
		{
			_health = Math.Clamp(_health - amount, 0, Stats.HealthMax);
			if (_health == 0)
				return true;
			return false;
		}

		public List<Tile> NeighboorTiles()
		{
			return _model.NeighboorTiles(this);
		}

		public bool MoveTo(Tile tile)
		{
			bool moved = _model.MoveTo(this, tile);
			if (moved)
				CurrentTile = tile;
			return moved;
		}

		public void SimulateStep()
		{
			_thirst = Math.Clamp(_thirst - Stats.ThirstRate, 0, ThirstMax);
			_hunger = Math.Clamp(_hunger - Stats.HungerRate, 0, HungerMax);
			_reproduceNeed = Math.Clamp(_reproduceNeed - Stats.ReproduceRate, 0, ReproduceNeedMax);

			bool healthLost = false;
			if(_thirst == 0)
			{
				_health -= HealthDamageThirst;
				healthLost = true;
			}
			if(_hunger == 0)
			{
				_health -= HealthDamageHunger;
				healthLost = true;
			}
			if(_reproduceNeed == 0)
			{
				_health -= HealthDamageReproduce;
				healthLost = true;
			}

			if (_health <= 0 || _age >= Stats.Age)
			{
				Die();
			}
			else if(!healthLost)
			{
				_health = Math.Clamp(_health + Stats.HealingRate, 0, Stats.HealthMax);
			}

			CurrentBehavior ??= SelectBehavior();
			CurrentBehavior = CurrentBehavior.DoBehavior() ? null : CurrentBehavior;

			_stepsCurrentTask++;
			if (_stepsCurrentTask > BehaviorAttentionSpan)
			{
				CurrentBehavior = null;
				_stepsCurrentTask = 0;
			}
			_age++;
		}

		public IBehavior SelectBehavior()
		{
			if(!ThirstSatisfied())
			{
				return new SearchWaterBehavior(this);
			}
			if(!HungerSatisfied())
			{
				if (SimulationModel.Generator.NextDouble() < Stats.Carnivorousness)
					return new HuntBehavior(this);
				else
					return new SearchFoodBehavior(this);
			}
			if(!ReproduceSatisfied())
			{
				return new ReproduceBehavior(this);
			}

			return new IdleBehavior(this);
		}

		public void GiveBirth(CreatureEntity pair)
		{
			CreatureEntity baby = new(_model, Gene.CreateChild(pair.Gene, this.Gene), CurrentTile);
			bool givenBirth = _model.NotifyBorn(baby);
			if (!givenBirth)
				throw new Exception("Tried to give birth, but didn't");
		}

		private void Die()
		{
			bool died = _model.NotifyDeath(this);
			if (!died)
				throw new Exception("Tried to die, but didn't");
		}
	}
}
 