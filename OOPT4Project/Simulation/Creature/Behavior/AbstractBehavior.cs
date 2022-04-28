using OOPT4Project.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public abstract class AbstractBehavior : IBehavior
	{
		public abstract bool DoBehavior();

		protected static double MoveRandomTileChance = 0.4;
		protected void MoveRandom(CreatureEntity creature)
		{
			if (SimulationModel.Generator.NextDouble() < MoveRandomTileChance)
			{
				var tiles = creature.NeighboorTiles();
				var tile = tiles.PickRandom(SimulationModel.Generator);
				creature.MoveTo(tile);
			}
		}
		protected void MoveRandom(CreatureEntity creature, double chance)
		{
			if (SimulationModel.Generator.NextDouble() < chance)
			{
				var tiles = creature.NeighboorTiles();
				var tile = tiles.PickRandom(SimulationModel.Generator);
				creature.MoveTo(tile);
			}
		}
	}
}
