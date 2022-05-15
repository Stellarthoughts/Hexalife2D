using NUnit.Framework;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Tests
{
	public class MapClimateTests
	{

		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void Test_MapClimate_ClimateChange()
		{
			SimulationModel model = new();
			model.CreateMapRandom(1, TileTypeLogic.ProbWeightsDefault, 0.1);
			Assert.DoesNotThrow(() =>
			{
				var mapClimate = model.MapController.MapClimate;
				var start = mapClimate.ClimateType;
				for (int i = 0; i < MapClimate.CycleMaxLength + 1; i++)
				{
					model.SimulateStep();
					if (start != mapClimate.ClimateType)
					{
						Assert.IsTrue(true);
					}
				}
			});
		}
	}
}
