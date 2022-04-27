namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class HuntBehavior : IBehavior
	{
		private CreatureEntity _creatureEntity;

		public HuntBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public bool DoBehavior()
		{
			return true;
		}
	}
}
