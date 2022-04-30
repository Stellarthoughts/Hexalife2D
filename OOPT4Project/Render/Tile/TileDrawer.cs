using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Render
{
	public class TileDrawer : IDrawer
	{
		private double _tileSize;

		public TileDrawer(List<Tile> _tiles, double tileSize)
		{
			_tileSize = tileSize;
		}

		public void Draw(ICanvas canvas, CanvasCamera camera)
		{
			//throw new NotImplementedException();
		}

		public void RecalculateOffset()
		{
			//throw new NotImplementedException();
		}

		public PathF PathTile(Point centerGlobal, Coordinates coor) => PathTile(centerGlobal, coor, _tileSize);

		public static PathF PathTile(Point centerGlobal, Coordinates coor, double tileSize)
		{
			PathF path = new PathF();
			Point hexToPixel = HexToPixel(coor, tileSize);
			Point centerTile = new Point(hexToPixel.X + centerGlobal.X, hexToPixel.Y + centerGlobal.Y);

			path.MoveTo(AnglePoint(centerTile, 0, tileSize));
			for (int i = 1; i <= 5; i++)
			{
				path.LineTo(AnglePoint(centerTile, i, tileSize));
			}
			path.Close();
			return path;
		}

		public PathF PathTile(PathF path, Coordinates coor)
		{
			Point hexToPixel = HexToPixel(coor, _tileSize);
			PathF res = new PathF(path);
			res.Equals(path);
			res.Move((float)hexToPixel.X, (float)hexToPixel.Y);
			return res;
		}

		public static Point AnglePoint(Point centerTile, int i, double tileSize)
		{
			double angle_deg = 60 * i;
			double angle_rad = Math.PI / 180 * angle_deg;
			return new Point(centerTile.X + tileSize * Math.Cos(angle_rad),
							 centerTile.Y + tileSize * Math.Sin(angle_rad));
		}

		public static Point HexToPixel(Coordinates coor, double tileSize)
		{
			double x = tileSize * (3.0 / 2 * coor.q);
			double y = tileSize * (Math.Sqrt(3) / 2 * coor.q + Math.Sqrt(3) * coor.r);
			return new Point(x, y);
		}
		public static Point HexToPixel(double q, double r, double tileSize)
		{
			double x = tileSize * (3.0 / 2 * q);
			double y = tileSize * (Math.Sqrt(3) / 2 * q + Math.Sqrt(3) * r);
			return new Point(x, y);
		}

		public static Coordinates PixelToHex(Point point, double tileSize)
		{
			double q = (2.0 / 3 * point.X) / tileSize;
			double r = (-1.0 / 3 * point.X + Math.Sqrt(3) / 3 * point.Y) / tileSize;

			int q_r = (int)Math.Round(q);
			int r_r = (int)Math.Round(r);

			return new Coordinates(0, 0);
		}

		public static double InscribedCircleRadius(double tileSize)
		{
			return tileSize * Math.Sqrt(3) / 2;
		}

		public static Point AvgHexCoordinates(List<Coordinates> coor, double tileSize)
		{
			double avgQ = 0;
			double avgR = 0;
			coor.ForEach(x =>
			{
				avgQ += x.q;
				avgR += x.r;
			});
			avgQ /= coor.Count;
			avgR /= coor.Count;

			return TileDrawer.HexToPixel(avgQ, avgR, tileSize);
		}
	}
}
