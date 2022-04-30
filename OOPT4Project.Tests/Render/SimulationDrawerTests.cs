using Moq;
using NUnit.Framework;
using OOPT4Project.Render;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using SkiaSharp;

namespace OOPT4Project.Tests
{
	public class SimulationDrawerTests
	{
		private Mock<SKCanvas>? _canvas;

		[SetUp]
		public void Setup()
		{
			_canvas = new();
		}

		[Test]
		public void Test_MapController_CreatesMap()
		{
			SimulationModel model = new();
			model.CreateMapRandom(10, TileTypeLogic.ProbWeightsDefault, 0.1);
			model.PopulateSimulation(10);
			SimulationDrawer drawer = new(model, 10);
			drawer.Draw(_canvas!.Object, new CanvasCamera(new CameraSettings(0, 0, 0, 0)));
			Assert.Positive(_canvas.Invocations.Count);
		}
	}
}