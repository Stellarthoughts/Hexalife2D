using OOPT4Project.Simulation.Map;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Windows;

namespace OOPT4Project.Render
{
	public class TileDrawer : ISimulationDrawer
	{
		private readonly float _tileSize;
		private readonly List<Tile> _tiles;
		private Tile _selectedTile = null!;
		private CanvasCamera _lastCamera = null!;

		public TileDrawer(List<Tile> tiles, float tileSize)
		{
			_tiles = tiles;
			_tileSize = tileSize;
		}

		private static readonly SKPaint TilePaintStroke = new()
		{
			Style = SKPaintStyle.Stroke,
			Color = SKColors.Black,
			StrokeWidth = 1f,
			IsAntialias = true,
		};

		private static readonly SKPaint TilePaintSelectedStroke = new()
		{
			Style = SKPaintStyle.Stroke,
			Color = SKColors.Red,
			StrokeWidth = 2f,
			IsAntialias = true,
		};

		public void Draw(SKCanvas canvas, CanvasCamera camera)
		{
			_lastCamera = camera;

			foreach (Tile tile in _tiles)
			{
				if (tile == _selectedTile) 
					continue;
				DrawTile(tile, TilePaintStroke, canvas, camera);
			}
			if(_selectedTile != null)
			{
				DrawTile(_selectedTile, TilePaintSelectedStroke, canvas, camera);
			}
		}
		private void DrawTile(Tile tile, SKPaint stroke, SKCanvas canvas, CanvasCamera camera)
		{
			SKPath path = PathTile(tile.Coordinates);
			camera.Adjust(ref path);
			TileColors.TileTypeToColor.TryGetValue(tile.Type, out SKColor color);

			SKPaint paint = new()
			{
				Style = SKPaintStyle.Fill,
				Color = color,
			};

			canvas.DrawPath(path, paint);
			canvas.DrawPath(path, stroke);
		}

		public void SelectTile(Tile tile)
		{
			_selectedTile = tile;
		}

		public SKPath PathTile(Coordinate coor) => PathTile(coor, _tileSize);

		public static SKPath PathTile(Coordinate coor, float tileSize)
		{
			SKPath path = new SKPath();
			SKPoint hexToPixel = HexToPixel(coor, tileSize);
			SKPoint centerTile = new SKPoint(hexToPixel.X, hexToPixel.Y);

			path.MoveTo(AnglePoint(centerTile, 0, tileSize));
			for (int i = 1; i <= 5; i++)
			{
				path.LineTo(AnglePoint(centerTile, i, tileSize));
			}
			path.Close();
			return path;
		}

		public SKPath PathTile(SKPath path, Coordinate coor)
		{
			SKPoint hexToPixel = HexToPixel(coor, _tileSize);
			SKPath res = new SKPath(path);
			res.Equals(path);
			res.Offset((float)hexToPixel.X, (float)hexToPixel.Y);
			return res;
		}

		public static SKPoint AnglePoint(SKPoint centerTile, int i, float tileSize)
		{
			float angle_deg = 60 * i;
			float angle_rad = MathF.PI / 180 * angle_deg;
			return new SKPoint(centerTile.X + tileSize * MathF.Cos(angle_rad),
							 centerTile.Y + tileSize * MathF.Sin(angle_rad));
		}

		public static SKPoint HexToPixel(Coordinate coor, float tileSize)
		{
			float x = tileSize * (3f / 2 * coor.q);
			float y = tileSize * (MathF.Sqrt(3) / 2 * coor.q + MathF.Sqrt(3) * coor.r);
			return new SKPoint(x, y);
		}

		public static SKPoint HexToPixel(float q, float r, float tileSize)
		{
			float x = tileSize * (3f / 2 * q);
			float y = tileSize * (MathF.Sqrt(3) / 2 * q + MathF.Sqrt(3) * r);
			return new SKPoint(x, y);
		}

		public Tile? GetTileFromPixel(Point point)
		{
			if (_lastCamera == null)
				return null;

			_lastCamera.AdjustReverse(ref point);

			Coordinate coor = PixelToHex(point, _tileSize);
			var tile = _tiles.Find(x => x.Coordinates.Equals(coor));
			return tile;
		}

		public static Coordinate PixelToHex(Point point, float tileSize)
		{
			double q = (2.0f / 3 * point.X) / tileSize;
			double r = (-1.0f / 3 * point.X + MathF.Sqrt(3) / 3 * point.Y) / tileSize;

			int q_r = (int)Math.Round(q);
			int r_r = (int)Math.Round(r);

			return new Coordinate(q_r, r_r);
		}

		public static float InscribedCircleRadius(float tileSize)
		{
			return tileSize * MathF.Sqrt(3) / 2;
		}

		public static SKPoint AvgHexCoordinates(List<Coordinate> coor, float tileSize)
		{
			float avgQ = 0;
			float avgR = 0;
			coor.ForEach(x =>
			{
				avgQ += x.q;
				avgR += x.r;
			});
			avgQ /= coor.Count;
			avgR /= coor.Count;

			return HexToPixel(avgQ, avgR, tileSize);
		}
	}
}
