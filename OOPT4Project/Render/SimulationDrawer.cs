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

		private double _tileSize;

		private Point _offset;
		private List<Tile> _tiles;
		private Dictionary<Tile, Color> _tileColors;

		public SimulationDrawer(SimulationModel simulationModel, double tileSize)
		{
			_simulationModel = simulationModel;
			_tileSize = tileSize;
			_tiles = _simulationModel.MapController.TileList;

			_tileColors = AssignColors(_tiles);
			_offset = AvgHexCoordinates(_tiles, _tileSize);
		}

		public static Dictionary<Tile, Color> AssignColors(List<Tile> tiles)
		{
			Dictionary<Tile, Color> dictionary = new();
			foreach (Tile tile in tiles)
			{
				Color? color;
				try
				{
					TileColors.TileTypeToColor.TryGetValue(tile.Type, out color);
				}
				catch (ArgumentNullException ex)
				{
					Console.WriteLine(ex.Message);
					color = Colors.Black;
				}
				dictionary.Add(tile, color!);
			}
			return dictionary;
		}

		public void Draw(ICanvas canvas, double width, double height)
		{
			foreach (Tile tile in _tiles)
			{
				PathF path = TileDrawer.PathTile(new Point(width/2 - _offset.X, height/2 - _offset.Y), tile.Coordinates, _tileSize);
				_tileColors.TryGetValue(tile, out Color? color);

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
