using Microsoft.Maui.Graphics;
using OOPT4Project.Render.Camera;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Render
{
	public class SimulationDrawer
	{
		private SimulationModel _simulationModel;

		private readonly double _tileSize;

		private Point _offset;
		private readonly List<Tile> _tiles;
		private Dictionary<Tile, Color> _tileColors = null!;

		public SimulationDrawer(SimulationModel simulationModel, double tileSize)
		{
			_simulationModel = simulationModel;
			_tileSize = tileSize;
			_tiles = _simulationModel.MapController.TileList;

			Init();
		}

		public void Init()
		{
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
		public void Draw(ICanvas canvas, CanvasCamera camera)
		{
			int hunterCount = 0;
			int preyCount = 0;
			foreach (Tile tile in _tiles)
			{
				PathF path = TileDrawer.PathTile(new Point(-_offset.X, -_offset.Y), tile.Coordinates, _tileSize);
				camera.Adjust(ref path);
				_tileColors.TryGetValue(tile, out Color? color);

				canvas.FillColor = color;
				canvas.StrokeColor = Colors.Black;
				canvas.StrokeSize = 1f;
				canvas.FillPath(path);
				canvas.DrawPath(path);

				var creatureList = tile.CreatureList;

				foreach (CreatureEntity crt in creatureList)
				{
					bool hunter = crt.Gene.Genom[1] > 0.5;
					if (hunter)
						hunterCount++;
					else
						preyCount++;
					canvas.FillColor = hunter ? Colors.Red : Colors.Green;
					
					canvas.StrokeColor = Colors.Black;
					canvas.StrokeSize = 0.7f;

					// Pos calculation
					Point tilePoint = TileDrawer.HexToPixel(crt.CurrentTile.Coordinates, _tileSize);
					tilePoint = tilePoint.Offset(-_offset.X, -_offset.Y);

					double position = creatureList.IndexOf(crt);
					double count = creatureList.Count;
					double circle = TileDrawer.InscribedCircleRadius(_tileSize) / 1.5;

					if(count != 1)
						tilePoint = tilePoint.Offset(Math.Cos(Math.PI * 2 * position/count) * circle, Math.Sin(Math.PI * 2 * position / count) * circle);

					Size size = new(_tileSize / count / 2);
					tilePoint -= size / 2;

					// Adjusting
					camera.Adjust(ref tilePoint);
					camera.Adjust(ref size);

					Rect rect = new Rect(tilePoint, size);

					canvas.DrawRectangle(rect);
					canvas.FillRectangle(rect);
				}
			}
			canvas.DrawString(hunterCount.ToString(), 300, 20, HorizontalAlignment.Center);
			canvas.DrawString(preyCount.ToString(), 350, 20, HorizontalAlignment.Center);
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
