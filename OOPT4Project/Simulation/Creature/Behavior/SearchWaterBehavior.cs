using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class SearchWaterBehavior : AbstractBehavior
	{
		private CreatureEntity _creatureEntity;

		public SearchWaterBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public override bool DoBehavior()
		{
			double thisrt = _creatureEntity.ThirstValue();
			Tile tile = _creatureEntity.CurrentTile;

			if (tile.GetWaterCount() > 0)
			{
				double amount = tile.EatAmount(thisrt);
				_creatureEntity.SatisfyThirst(amount);
			}
			else
			{
				var neighboors = _creatureEntity.NeighboorTiles();
				var neighboorMaxWater = neighboors.MaxBy(x => x.GetWaterCount());
				if (neighboorMaxWater != null)
				{
					_creatureEntity.MoveTo(neighboorMaxWater);
				}
				else
				{
					MoveRandom(_creatureEntity, 1);
				}
			}

			if (_creatureEntity.ThirstSatisfied())
				return true;
			else
				return false;
		}
	}
}
