using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class SearchFoodBehavior : AbstractBehavior
	{
		private readonly CreatureEntity _creature;

		public SearchFoodBehavior(CreatureEntity creatureEntity)
		{
			_creature = creatureEntity;
		}

		public override bool DoBehavior()
		{
			double hunger = _creature.HungerValue();
			Tile tile = _creature.CurrentTile;

			if (tile.GetFoodCount() > 0)
			{
				double amount = tile.EatAmount(hunger);
				_creature.SatisfyHunger(amount);
			}
			else
			{
				MoveToNeighboorMaxBy(_creature, x => x.GetFoodCount());
			}

			if (_creature.HungerSatisfied())
				return true;
			else
				return false;
		}
	}
}
