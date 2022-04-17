using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;
using System;

namespace OOPT4Project.Simulation.Creature
{
	public class CreatureEntity : ISimulated
	{
		public Gene Gene { get; private set; }
		public IBehavior? CurrentBehavior { get; private set; }
		public Tile CurrentTile { get; set; }

		public double _hunger = 100;
		public double _thirst = 100;
		public double _reproduceNeed = 0;

		public CreatureEntity(Gene gene, Tile tile)
		{
			Gene = gene;
			CurrentTile = tile;
		}

		public void SimulateStep()
		{
			if(CurrentBehavior == null)
			{
				CurrentBehavior = SelectBehavior();
			}
			else
			{
				var done = CurrentBehavior.DoBehavior();
				if (done)
					CurrentBehavior = null;
			}	
		}
		public IBehavior SelectBehavior()
		{
			return new SearchBehavior(this);
		}
	}
}
