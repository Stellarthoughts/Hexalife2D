namespace OOPT4Project.Simulation.Creature
{
	public class CreatureEventArgs
	{
		public CreatureEntity Creature { get; private set; }

		public CreatureEventArgs(CreatureEntity creature)
		{
			Creature = creature;
		}
	}
}
