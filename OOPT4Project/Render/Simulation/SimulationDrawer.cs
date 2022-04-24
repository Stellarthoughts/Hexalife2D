using Microsoft.Maui.Graphics;
using OOPT4Project.Render.Camera;
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
				TileColors.TileTypeToColor.TryGetValue(tile.Type, out Color? color);
				color ??= Colors.Black;
				dictionary.Add(tile, color!);
			}
			return dictionary;
		}

		// TODO: OPTIMIZE by storing single template of PathF, then move it and resize;
		public void Draw(ICanvas canvas, CanvasCamera camera, Point point)
		{
			foreach (Tile tile in _tiles)
			{
				PathF path = TileDrawer.PathTile(new Point(point.X - _offset.X, point.Y - _offset.Y), tile.Coordinates, _tileSize);
				camera.Adjust(ref path);
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
