using Moq;
using NUnit.Framework;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Tests
{
	public class MapControllerTests
	{
		private Mock<SimulationModel>? simulationModel;
		private MapController mapController = null!;

		[SetUp]
		public void Setup()
		{
			simulationModel = new Mock<SimulationModel>();
			mapController = new(simulationModel.Object);
		}

		[Test]
		public void Test_MapController_CreatesMap()
		{
			int resource = 100;
			mapController.CreateMapRandom(resource, TileTypeLogic.ProbWeightsDefault, 0.1);
			Assert.That(mapController.TileList.Count > resource);
		}
	}
}