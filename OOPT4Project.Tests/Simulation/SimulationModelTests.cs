using NUnit.Framework;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Tests
{
	internal class SimulationModelTests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test_SimulationModel_Simulated()
		{
			SimulationModel model = new();
			model.CreateMapRandom(10, TileTypeLogic.ProbWeightsDefault, 0.1);
			model.PopulateSimulation(10);
			Assert.DoesNotThrow(() =>
			{
				for (int i = 0; i < 10; i++)
					model.SimulateStep();
			});
		}
	}
}
