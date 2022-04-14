using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;
using System;

namespace OOPT4Project.Simulation.Creature
{
	public class CreatureEntity : ISimulated
	{
		public Gene Gene { get; private set; }
		public Stats Stats { get; private set; }

		public IBehavior? CurrentBehavior { get; private set; }

		public Tile CurrentTile { get; set; }

		public CreatureEntity(Gene gene, Tile tile)
		{
			Gene = gene;
			CurrentTile = tile;
			Stats = gene.CreateStats();
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
