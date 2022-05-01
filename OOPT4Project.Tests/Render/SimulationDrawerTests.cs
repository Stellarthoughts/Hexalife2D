using NUnit.Framework;
using OOPT4Project.Render;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using SkiaSharp;

namespace OOPT4Project.Tests
{
	public class SimulationDrawerTests
	{
		private SKCanvas _canvas = null!;
		private readonly int _width = 500;
		private readonly int _height = 500;

		[SetUp]
		public void Setup()
		{
			_canvas = new(new(_width, _height));
		}

		[Test]
		public void Test_MapController_CreatesMap()
		{
			SimulationModel model = new();
			model.CreateMapRandom(10, TileTypeLogic.ProbWeightsDefault, 0.1);
			model.PopulateSimulation(10);
			SimulationDrawer drawer = new(model, 10);
			CanvasCamera camera = new CanvasCamera(new CameraSettings(0, 0, 0, 0));
			camera.SetGlobalOffset(_width / 2, _height / 2);
			camera.Update();
			drawer.Draw(_canvas!, camera);
			Assert.Positive(_canvas.TotalMatrix.Values.Length);
		}
	}
}