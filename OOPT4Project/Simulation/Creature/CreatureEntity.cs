using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Creature
{
	public struct CreatureStats
	{
		public double HealthMax { get; set; }
		public double HungerRate { get; set; }
		public double ThirstRate { get; set; }
		public double Strength { get; set; }
		public double ReproduceRate { get; set; }
		public double Carnivorousness { get; set; }
		public double Stealth { get; set; }
		public double Awareness { get; set; }
		public double Size { get; set; }
		public double HealingRate { get; set; }
	}

	public class CreatureEntity : ISimulated
	{
		public Gene Gene { get; private set; }
		public IBehavior? CurrentBehavior { get; private set; }
		public Tile CurrentTile { get; set; }

		private SimulationModel _model;
		public CreatureStats Stats { get; private set; }

		private static double HungerMax = 100;
		private static double ThirstMax = 100;
		private static double ReproduceNeedMax = 100;
		private static int AttentionSpan = 4;

		private double _health;
		private double _hunger = HungerMax;
		private double _thirst = ThirstMax;
		private double _reproduceNeed = ReproduceNeedMax;
		private int _stepsCurrentTask = 0;

		public CreatureEntity(SimulationModel model, Gene gene, Tile tile)
		{
			Gene = gene;
			CurrentTile = tile;
			_model = model;

			Stats = gene.GetStats();

			_health = Stats.HealthMax;
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
			_thirst = Math.Clamp(_thirst - Stats.ThirstRate, 0, HungerMax);
			_hunger = Math.Clamp(_thirst - Stats.HungerRate, 0, ThirstMax);
			_reproduceNeed = Math.Clamp(_reproduceNeed - Stats.ReproduceRate, 0, ReproduceNeedMax);

			if(_thirst == 0)
			{
				_health -= 15;
			}
			if(_hunger == 0)
			{
				_health -= 10;
			}
			if(_reproduceNeed == 0)
			{
				_health -= 5;
			}

			if (_health <= 0)
			{
				Die();
			}

			CurrentBehavior ??= SelectBehavior();
			CurrentBehavior = CurrentBehavior.DoBehavior() ? null : CurrentBehavior;

			_stepsCurrentTask++;
			if (_stepsCurrentTask > AttentionSpan)
			{
				CurrentBehavior = null;
				_stepsCurrentTask = 0;
			}
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

		private void Die()
		{
			bool died = _model.NotifyDeath(this);
			if (!died)
				throw new Exception("Tried to die, but didn't");
		}

		public void GiveBirth(CreatureEntity pair)
		{
			CreatureEntity baby = new(_model, Gene.CreateChild(pair.Gene, this.Gene), CurrentTile);
			bool givenBirth = _model.NotifyBorn(baby);
			if (!givenBirth)
				throw new Exception("Tried to give birth, but didn't");
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
	}
}
 