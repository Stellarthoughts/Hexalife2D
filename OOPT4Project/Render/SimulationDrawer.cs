using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Render
{
	public class SimulationDrawer
	{
		private SimulationModel _simulationModel;

		private double _tileSize = 15;

		public SimulationDrawer(SimulationModel simulationModel)
		{
			_simulationModel = simulationModel;
		}

		public void Draw(ICanvas canvas, double width, double height)
		{
			var tiles = _simulationModel.MapController.TileList;
			var offset = AvgHexCoordinates(tiles, _tileSize);

			foreach (Tile tile in tiles)
			{
				PathF path = TileDrawer.PathTile(new Point(width/2 - offset.X, height/2 - offset.Y), tile.Coordinates, _tileSize);
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

		public static Point AvgHexCoordinates(List<Tile> tiles, double tileSize)
		{
			double avgQ = 0;
			double avgR = 0;
			tiles.Select(x => x.Coordinates).ToList().ForEach(x => {
				avgQ += x.q;
				avgR += x.r;
			});
			avgQ /= tiles.Count;
			avgR /= tiles.Count;

			return TileDrawer.HexToPixel(avgQ,avgR,tileSize);
		}
	}
}
