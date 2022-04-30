using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Render
{
	public class CreatureDrawer : IDrawer
	{
		private readonly double _tileSize;
		private readonly List<Tile> _tiles;

		public CreatureDrawer(List<Tile> tiles, double tileSize)
		{
			_tiles = tiles;
			_tileSize = tileSize;
		}

		public void Draw(ICanvas canvas, CanvasCamera camera)
		{
			var tilesWithCreatures = _tiles.Where(x => x.CreatureList.Count > 0).ToList();
			foreach (Tile tile in tilesWithCreatures)
			{
				List<CreatureEntity> creatureList = tile.CreatureList;

				foreach (CreatureEntity creature in creatureList)
				{
					Point tilePoint = TileDrawer.HexToPixel(creature.CurrentTile.Coordinates, _tileSize);

					double position = creatureList.IndexOf(creature);
					double count = creatureList.Count;
					double circle = TileDrawer.InscribedCircleRadius(_tileSize) / 1.5;

					if (count != 1)
						tilePoint = tilePoint.Offset(
								Math.Cos(Math.PI * 2 * position / count) * circle,
								Math.Sin(Math.PI * 2 * position / count) * circle);

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
