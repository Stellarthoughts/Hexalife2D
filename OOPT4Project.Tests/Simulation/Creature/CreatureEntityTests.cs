using NUnit.Framework;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;

namespace OOPT4Project.Tests
{
	public class CreatureEntityTests
	{

		[SetUp]
		public void Setup()
		{

		}

		[Test]
		public void Test_CreatureEntity_SearchesForWaterAndFood()
		{
			bool food = false;
			bool water = false;

			SimulationModel model = new();
			model.CreateMapRandom(50, TileTypeLogic.ProbWeightsDefault, 0.1);
			model.PopulateSimulation(30);
			Assert.DoesNotThrow(() =>
			{
				for (int i = 0; i < 50; i++)
				{
					model.SimulateStep();
					foreach (Tile tile in model.MapController.TileList)
					{
						foreach (CreatureEntity creature in tile.CreatureList)
						{
							if (creature.CurrentBehavior is SearchWaterBehavior)
								water = true;
							if (creature.CurrentBehavior is SearchFoodBehavior || creature.CurrentBehavior is HuntBehavior)
								food = true;
						}
					}
				}
			});
			Assert.That(water && food);
		}
	}
}
