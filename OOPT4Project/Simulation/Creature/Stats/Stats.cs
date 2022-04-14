namespace OOPT4Project.Simulation.Creature
{
	public class Stats : ISimulated
	{
		public double Hunger { get; private set; } = 100;
		public double Thirst { get; private set; } = 100;
		public double ReproduceNeed { get; private set; } = 0;

		public void SimulateStep()
		{
			throw new System.NotImplementedException();
		}
	}
}
