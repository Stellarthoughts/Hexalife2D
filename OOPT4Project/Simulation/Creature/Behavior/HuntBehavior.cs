namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class HuntBehavior : AbstractBehavior
	{
		private CreatureEntity _creatureEntity;

		public HuntBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public override bool DoBehavior()
		{
			return true;
		}
	}
}
