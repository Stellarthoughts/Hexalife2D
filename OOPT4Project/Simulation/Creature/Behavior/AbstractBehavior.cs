using OOPT4Project.Extension;
using OOPT4Project.Simulation.Map;
using System;
using System.Linq;

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
			var neighboorMax = neighboors.MaxBy(p);
			if (neighboorMax != null)
			{
				creature.MoveTo(neighboorMax);
			}
			else
			{
				MoveRandom(creature, 1);
			}
		}
	}
}
