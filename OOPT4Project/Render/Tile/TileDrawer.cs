using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Map;
using System;

namespace OOPT4Project.Render
{
	public static class TileDrawer
	{
		public static PathF PathTile(Point centerGlobal, Coordinates coor, double size)
		{
			PathF path = new PathF();
			Point hexToPixel = HexToPixel(coor, size);
			Point centerTile = new Point(hexToPixel.X + centerGlobal.X,hexToPixel.Y + centerGlobal.Y);

			path.MoveTo(AnglePoint(centerTile, size, 0));
			for(int i = 1; i <= 5; i++)
			{
				path.LineTo(AnglePoint(centerTile, size, i));
			}
			path.Close();
			return path;
		}

		public static PathF PathTile(PathF path, Coordinates coor, double size)
		{
			Point hexToPixel = HexToPixel(coor, size);
			PathF res = new PathF(path);
			res.Equals(path);
			res.Move((float)hexToPixel.X, (float)hexToPixel.Y);
			return res;
		}

		public static Point AnglePoint(Point centerTile, double size, int i)
		{
			double angle_deg = 60 * i;
			double angle_rad = Math.PI / 180 * angle_deg;
			return new Point(centerTile.X + size * Math.Cos(angle_rad),
							 centerTile.Y + size * Math.Sin(angle_rad));
		}

		public static Point HexToPixel(Coordinates coor, double size)
		{
			double x = size * (3.0 / 2 * coor.q);
			double y = size * (Math.Sqrt(3) /2 * coor.q + Math.Sqrt(3)* coor.r);
			return new Point(x, y);
		}
		public static Point HexToPixel(double q, double r, double size)
		{
			double x = size * (3.0 / 2 * q);
			double y = size * (Math.Sqrt(3) / 2 * q + Math.Sqrt(3) * r);
			return new Point(x, y);
		}

		public static Coordinates PixelToHex(Point point, double size)
		{
			double q = (2.0 / 3 * point.X) / size;
			double r = (-1.0 / 3 * point.X + Math.Sqrt(3) / 3 * point.Y) / size;

			int q_r = (int)Math.Round(q);
			int r_r = (int)Math.Round(r);

			return new Coordinates(0, 0);
		}

	}
}
