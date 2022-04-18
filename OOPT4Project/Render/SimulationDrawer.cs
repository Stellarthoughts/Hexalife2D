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
			var tiles = _simulationModel.MapController.TileList;
			foreach(Tile tile in tiles)
			{
				PathF path = TileDrawer.PathTile(new Point(width/2, height/2), tile.Coordinates, _tileSize);
				Color? color;

				try
				{
					TileColors.TileTypeToColor.TryGetValue(tile.Type, out color);
				}
				catch(ArgumentNullException ex) 
				{ 
					Console.WriteLine(ex.Message);
					color = Colors.Black; 
				}

				canvas.FillColor = color;
				canvas.StrokeColor = Colors.Black;
				canvas.StrokeSize = 1f;
				canvas.FillPath(path);
				canvas.DrawPath(path);
			}
		}
	}
}
