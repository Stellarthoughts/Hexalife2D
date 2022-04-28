using OOPT4Project.Extension;
using OOPT4Project.Simulation.Map;
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

		protected static readonly double MoveRandomTileChance = 0.4;
		protected static void MoveRandom(CreatureEntity creature)
		{
			if (SimulationModel.Generator.NextDouble() < MoveRandomTileChance)
			{
				var tiles = creature.NeighboorTiles();
				var tile = tiles.PickRandom(SimulationModel.Generator);
				creature.MoveTo(tile);
			}
		}
		protected static void MoveRandom(CreatureEntity creature, double chance)
		{
			if (SimulationModel.Generator.NextDouble() < chance)
			{
				var tiles = creature.NeighboorTiles();
				var tile = tiles.PickRandom(SimulationModel.Generator);
				creature.MoveTo(tile);
			}
		}
		protected static void MoveToNeighboorMaxBy(CreatureEntity creature, Func<Tile, object> p)
		{
			var neighboors = creature.NeighboorTiles();
			var neighboorMaxFood = neighboors.MaxBy(p);
			if (neighboorMaxFood != null)
			{
				creature.MoveTo(neighboorMaxFood);
			}
			else
			{
				MoveRandom(creature, 1);
			}
		}
	}
}
