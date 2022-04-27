namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class SearchFoodBehavior : IBehavior
	{
		private CreatureEntity _creatureEntity;

		public SearchFoodBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public bool DoBehavior()
		{
			return true;
		}
	}
}
