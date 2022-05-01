using OOPT4Project.Extension;
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
		public CreatureType Type { get; private set; }
		public string UniqueName { get; private set; }

		private static readonly double HungerMax = 1;
		private static readonly double ThirstMax = 1;
		private static readonly double ReproduceNeedMax = 1;
		private static readonly int BehaviorAttentionSpan = 3;
		private static readonly double HealthDamageThirst = 0.15;
		private static readonly double HealthDamageHunger = 0.10;
		private static readonly double HealthDamageReproduce = 0.03;

		private readonly SimulationModel _model;

		private double _health;
		private double _hunger = HungerMax;
		private double _thirst = ThirstMax;
		private double _reproduceNeed = ReproduceNeedMax;
		private int _stepsCurrentTask = 0;
		private int _age = 0;
		private bool _healthLostStep = false;

		public event EventHandler? Death = null!;
		public event EventHandler? Born = null!;

		public delegate void DeathEvent(object sender, CreatureEventArgs e);

		public bool ThirstSatisfied() => _thirst > ThirstMax / 3;
		public bool HungerSatisfied() => _hunger > HungerMax / 4;
		public bool ReproduceSatisfied() => _reproduceNeed > ReproduceNeedMax / 5;
		public void SatisfyThirst(double amount) => _thirst = Math.Clamp(_thirst + amount, 0, ThirstMax);
		public void SatisfyHunger(double amount) => _hunger = Math.Clamp(_hunger + amount, 0, HungerMax);
		public void SatisfyReproduce(double amount) => _reproduceNeed = Math.Clamp(_reproduceNeed + amount, 0, ReproduceNeedMax);
		public double ThirstValue() => ThirstMax - _thirst;
		public double HungerValue() => HungerMax - _hunger;
		public double ReproduceValue() => ReproduceNeedMax - _reproduceNeed;

		public CreatureEntity(SimulationModel model, Gene gene, Tile tile)
		{
			Gene = gene;
			CurrentTile = tile;
			_model = model;

			Stats = gene.GetCreatureStats();
			Type = gene.GetCreatureType();

			_health = Stats.HealthMax;

			UniqueName = Type.ToString() + " " + NameGenerator.Generate(SimulationModel.Generator.Next(5, 10));
		}
		public bool DealDamage(double amount)
		{
			_healthLostStep = true;
			_health = Math.Clamp(_health - amount, 0, Stats.HealthMax);
			if (_health == 0)
				return true;
			return false;
		}

		public void Heal(double amount) => _health = Math.Clamp(_health + amount, 0, Stats.HealthMax);

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
			SatisfyThirst(-Stats.ThirstRate);
			SatisfyHunger(-Stats.HungerRate);
			SatisfyReproduce(-Stats.ReproduceRate);

			if (_thirst == 0)
				DealDamage(HealthDamageThirst);
			if (_hunger == 0)
				DealDamage(HealthDamageHunger);
			if (_reproduceNeed == 0)
				DealDamage(HealthDamageReproduce);

			if (_health <= 0 || _age >= Stats.Age)
				Die();
			else if (!_healthLostStep)
				Heal(Stats.HealingRate);

			CurrentBehavior ??= SelectBehavior();
			CurrentBehavior = CurrentBehavior.DoBehavior() ? null : CurrentBehavior;

			_stepsCurrentTask++;
			if (_stepsCurrentTask >= BehaviorAttentionSpan)
			{
				CurrentBehavior = null;
				_stepsCurrentTask = 0;
			}
			_age++;
			_healthLostStep = false;
		}

		public IBehavior SelectBehavior()
		{
			if (!ThirstSatisfied())
			{
				return new SearchWaterBehavior(this);
			}
			if (!HungerSatisfied())
			{
				if (SimulationModel.Generator.NextDouble() < Stats.Carnivorousness)
					return new HuntBehavior(this);
				else
					return new SearchFoodBehavior(this);
			}
			if (!ReproduceSatisfied())
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
