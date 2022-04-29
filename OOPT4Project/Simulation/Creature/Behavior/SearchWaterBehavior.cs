using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class SearchWaterBehavior : AbstractBehavior
	{
		private readonly CreatureEntity _creature;

		public SearchWaterBehavior(CreatureEntity creatureEntity)
		{
			_creature = creatureEntity;
		}

		public override bool DoBehavior()
		{
			double thisrt = _creature.ThirstValue();
			Tile tile = _creature.CurrentTile;

			if (tile.GetWaterCount() > 0)
			{
				double amount = tile.EatAmount(thisrt);
				_creature.SatisfyThirst(amount);
			}
			else
			{
				MoveToNeighboorMaxBy(_creature, x => x.GetWaterCount());
			}

			if (_creature.ThirstSatisfied())
				return true;
			else
				return false;
		}
	}
}
