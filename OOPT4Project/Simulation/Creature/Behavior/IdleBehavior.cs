namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class IdleBehavior : AbstractBehavior
	{
		private readonly CreatureEntity _creatureEntity;

		public IdleBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public override bool DoBehavior()
		{
			MoveRandom(_creatureEntity);
			return true;
		}
	}
}
