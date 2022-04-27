using OOPT4Project.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class IdleBehavior : IBehavior
	{
		private CreatureEntity _creatureEntity;

		private static double MoveRandomTileChance = 0.4;

		public IdleBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public bool DoBehavior()
		{
			if(SimulationModel.Generator.NextDouble() < MoveRandomTileChance)
			{
				var tiles = _creatureEntity.NeighboorTiles();
				var tile = tiles.PickRandom(SimulationModel.Generator);
				_creatureEntity.MoveTo(tile);
			}
			return true;
		}
	}
}
