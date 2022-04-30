using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Platform;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OOPT4Project.Render
{
	public class SimulationDrawer
	{
		private readonly SimulationModel _simulationModel;
		private readonly double _tileSize;
		private Point _offset;
		private List<Tile> _tiles;

		private TileDrawer _tileDrawer;
		private CreatureDrawer _creatureDrawer;

		public SimulationDrawer(SimulationModel simulationModel, double tileSize)
		{
			_simulationModel = simulationModel;
			_tileSize = tileSize;
			_tiles = _simulationModel.MapController.TileList;
			_tileDrawer = new(_tiles, tileSize);
			_creatureDrawer = new(_tiles, tileSize);

			Init();
		}

		public void Init()
		{
			_tiles = _simulationModel.MapController.TileList;
			_tileDrawer.RecalculateOffset();
		}

		// TODO: OPTIMIZE by storing single template of PathF, then move it and resize;
		public void Draw(ICanvas canvas, CanvasCamera camera)
		{
			_tileDrawer.Draw(canvas, camera);
			_creatureDrawer.Draw(canvas, camera);
			foreach (Tile tile in _tiles)
			{
				PathF path = _tileDrawer.PathTile(new Point(-_offset.X, -_offset.Y), tile.Coordinates);
				camera.Adjust(ref path);
				TileColors.TileTypeToColor.TryGetValue(tile.Type, out Color? color);

				canvas.FillColor = color ?? Colors.Black;
				canvas.StrokeColor = Colors.Black;
				canvas.StrokeSize = 0.5f;
				canvas.FillPath(path);
				canvas.DrawPath(path);

				var creatureList = tile.CreatureList;

				foreach (CreatureEntity crt in creatureList)
				{
					// Pos calculation
					Point tilePoint = TileDrawer.HexToPixel(crt.CurrentTile.Coordinates, _tileSize);
					tilePoint = tilePoint.Offset(-_offset.X, -_offset.Y);

					double position = creatureList.IndexOf(crt);
					double count = creatureList.Count;
					double circle = TileDrawer.InscribedCircleRadius(_tileSize) / 1.5;

					if (count != 1)
						tilePoint = tilePoint.Offset(Math.Cos(Math.PI * 2 * position / count) * circle, Math.Sin(Math.PI * 2 * position / count) * circle);

					Size size = new(_tileSize / count / 2);
					tilePoint -= size / 2;

					// Adjusting
					camera.Adjust(ref tilePoint);
					camera.Adjust(ref size);

					Rect rect = new Rect(tilePoint, size);

					canvas.FillColor = Colors.Red;
					canvas.StrokeColor = Colors.Black;
					canvas.StrokeSize = 0.5f;
					canvas.FillRectangle(rect);
					canvas.DrawRectangle(rect);
				}
			}
		}
	}
}
