using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using System;

namespace OOPT4Project.Render
{
	public class SimulationDrawer
	{
		private SimulationModel _simulationModel;

		private double _tileSize = 20;

		public SimulationDrawer(SimulationModel simulationModel)
		{
			_simulationModel = simulationModel;
		}

		public void Draw(ICanvas canvas, double width, double height)
		{
			var tiles = _simulationModel.MapController.Tiles;
			foreach(Tile tile in tiles)
			{
				PathF path = DrawTile.PathTile(new Point(width/2, height/2), tile.Coordinates, _tileSize);
				canvas.FillColor = Color.FromArgb("51C65C");
				canvas.StrokeColor = Color.FromArgb("000000");
				canvas.StrokeSize = 1;
				canvas.FillPath(path);
				canvas.DrawPath(path);
			}
		}
	}
}
