using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Map;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Render
{
	public class CreatureDrawer : ISimulationDrawer
	{
		private readonly float _tileSize;
		private readonly List<Tile> _tiles;

		public CreatureDrawer(List<Tile> tiles, float tileSize)
		{
			_tiles = tiles;
			_tileSize = tileSize;
		}

		private static readonly SKPaint DefaultCreaturePaintFill = new()
		{
			Style = SKPaintStyle.Fill,
			Color = SKColors.Red,
		};

		private static readonly SKPaint DefaultCreaturePaintStroke = new()
		{
			Style = SKPaintStyle.Stroke,
			Color = SKColors.Black,
			StrokeWidth = 0.5f,
			IsAntialias = true,
		};

		private static readonly SKPaint DefaultTextFill = new()
		{
			Style = SKPaintStyle.Fill,
			Color = SKColors.Black,
			TextAlign = SKTextAlign.Center,
		};

		public void Draw(SKCanvas canvas, CanvasCamera camera)
		{
			var tilesWithCreatures = _tiles.Where(x => x.CreatureList.Count > 0).ToList();

			foreach (Tile tile in tilesWithCreatures)
			{
				List<CreatureEntity> creatureList = tile.CreatureList;
				SKPoint tilePoint = TileDrawer.HexToPixel(tile.Coordinates, _tileSize);

				if (creatureList.Count < 10)
				{
					foreach (CreatureEntity creature in creatureList)
					{
						SKPoint creaturePoint = new(tilePoint.X, tilePoint.Y);
						CreatureImages.CreatureTypeToImage.TryGetValue(creature.Type, out var bitmap);
						var image = SKImage.FromBitmap(bitmap);

						if (bitmap != null)
						{
							// Positioning
							OffsetRadial(ref creaturePoint, creatureList.IndexOf(creature), creatureList.Count);
							AdjustBySize(ref creaturePoint, ref bitmap, 1f / creatureList.Count);

							// Adjusting
							camera.Adjust(ref creaturePoint);
							camera.Adjust(ref bitmap);

							canvas.DrawBitmap(bitmap, creaturePoint);
						}
					}
				}
				else
				{
					SKPoint textPoint = new(tilePoint.X, tilePoint.Y);
					camera.Adjust(ref textPoint);
					canvas.DrawText(creatureList.Count.ToString(), textPoint, DefaultTextFill);
				}
			}
		}

		private void OffsetRadial(ref SKPoint point, int position, int count)
		{
			float circle = TileDrawer.InscribedCircleRadius(_tileSize) / 1.7f;
			if (count != 1)
				point.Offset(
						MathF.Cos(MathF.PI * 2 * position / count) * circle,
						MathF.Sin(MathF.PI * 2 * position / count) * circle);
		}

		private void AdjustBySize(ref SKPoint point, ref SKBitmap bitmap, float factor)
		{
			float sizeX = bitmap.Width * factor;
			float sizeY = bitmap.Height * factor;
			SKBitmap scaled = new((int)sizeX, (int)sizeY);
			bitmap.ScalePixels(scaled, SKFilterQuality.High);
			bitmap = scaled;
			point.Offset(-sizeX / 2, -sizeY / 2);
		}
	}
}
