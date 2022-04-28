using OOPT4Project.Simulation.Map;
using System.Linq;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class SearchFoodBehavior : AbstractBehavior
	{
		private CreatureEntity _creatureEntity;

		public SearchFoodBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public override bool DoBehavior()
		{
			double hunger = _creatureEntity.HungerValue();
			Tile tile = _creatureEntity.CurrentTile;

			if(tile.GetFoodCount() > 0)
			{
				double amount = tile.EatAmount(hunger);
				_creatureEntity.SatisfyHunger(amount);
			}
			else
			{
				var neighboors = _creatureEntity.NeighboorTiles();
				var neighboorMaxFood = neighboors.MaxBy(x => x.GetFoodCount());
				if(neighboorMaxFood != null)
				{
					_creatureEntity.MoveTo(neighboorMaxFood);
				}
				else
				{
					MoveRandom(_creatureEntity, 1);
				}
			}

			if (_creatureEntity.HungerSatisfied())
				return true;
			else
				return false;
		}
	}
}
