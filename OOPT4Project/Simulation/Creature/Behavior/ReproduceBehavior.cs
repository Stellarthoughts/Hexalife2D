namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class ReproduceBehavior : IBehavior
	{
		private CreatureEntity _creatureEntity;

		public ReproduceBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public bool DoBehavior()
		{
			return true;
		}
	}
}
