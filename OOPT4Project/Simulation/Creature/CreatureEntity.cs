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
		public double HungerMax { get; set; }
		public double HungerMin { get; set; }
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

			_health = Stats.HealthMax;

			Stats = gene.GetStats();
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
			_thirst -= Stats.ThirstRate;
			_hunger -= Stats.HungerRate;
			_reproduceNeed -= Stats.ReproduceRate;

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
				//return;
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

		private void Die()
		{
			
		}

		public IBehavior SelectBehavior()
		{
			/*if(_thirst <= ThirstMax / 3)
			{
				return new SearchWaterBehavior(this);
			}
			if(_hunger <= HungerMax / 4)
			{
				if (SimulationModel.Generator.NextDouble() < Stats.Carnivorousness)
					return new HuntBehavior(this);
				else
					return new SearchFoodBehavior(this);
			}
			if(_reproduceNeed <= ReproduceNeedMax / 5)
			{
				return new ReproduceBehavior(this);
			}*/

			return new IdleBehavior(this);
		}
	}
}
 