namespace OOPT4Project.Simulation.Creature
{
	public delegate void CreatureEvent(object sender, CreatureEventArgs e);
	public class CreatureEventArgs
	{
		public CreatureEntity Creature { get; private set; }

		public CreatureEventArgs(CreatureEntity creature)
		{
			Creature = creature;
		}
	}
}
