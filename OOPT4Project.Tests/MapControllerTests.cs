using Moq;
using NUnit.Framework;
using OOPT4Project.Simulation;

namespace OOPT4Project.Tests
{
	public class MapControllerTests
	{
		public Mock<SimulationModel>? SimulationModel;
		[SetUp]
		public void Setup()
		{
			SimulationModel = new Mock<SimulationModel>();
		}

		[Test]
		public void Test1()
		{
			Assert.Pass();
		}
	}
}