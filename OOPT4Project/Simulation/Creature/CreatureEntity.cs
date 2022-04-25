using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Simulation.Creature
{
	public struct CreatureStats
	{
		public double Health { get; set; }
		public double HungerRate { get; set; }
		public double ThirstRate { get; set; }
		public double Strength { get; set; }
		public double ReproduceRate { get; set; }
		public double Carnivorousness { get; set; }
		public double Stealth { get; set; }
		public double Awareness { get; set; }
		public double Size { get; set; }
	}

	public class CreatureEntity : ISimulated
	{
		public Gene Gene { get; private set; }
		public IBehavior? CurrentBehavior { get; private set; }
		public Tile CurrentTile { get; set; }

		private SimulationModel _model;
		public CreatureStats Stats { get; private set; }

		private double _hunger = 100;
		private double _thirst = 100;
		private double _reproduceNeed = 0;

		public CreatureEntity(SimulationModel model, Gene gene, Tile tile)
		{
			Gene = gene;
			CurrentTile = tile;
			_model = model;

			Stats = gene.GetStats();
		}

		public void SimulateStep()
		{
			CurrentBehavior ??= SelectBehavior();
			CurrentBehavior = CurrentBehavior.DoBehavior() ? null : CurrentBehavior;
		}
		public IBehavior SelectBehavior()
		{
			return new SearchBehavior(this);
		}
	}
}
 