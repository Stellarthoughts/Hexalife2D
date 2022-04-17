using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Map;
using System;

namespace OOPT4Project.Render
{
	public static class TileDrawer
	{
		public static PathF PathTile(Point center_global, Coordinates coor, double size)
		{
			PathF path = new PathF();
			Point hexToPixel = HexToPixel(coor, size);
			Point tileCenter = new Point(hexToPixel.X + center_global.X,hexToPixel.Y + center_global.Y);

			path.MoveTo(AnglePoint(tileCenter, size,0));
			for(int i = 1; i <= 6; i++)
			{
				path.LineTo(AnglePoint(tileCenter, size, i));
			}
			return path;
		}

		public static Point AnglePoint(Point center_tile, double size, int i)
		{
			double angle_deg = 60 * i;
			double angle_rad = Math.PI / 180 * angle_deg;
			return new Point(center_tile.X + size * Math.Cos(angle_rad),
							 center_tile.Y + size * Math.Sin(angle_rad));
		}

		public static Point HexToPixel(Coordinates coor, double size)
		{
			double x = size * (3.0 / 2 * coor.q);
			double y = size * (Math.Sqrt(3) /2 * coor.q + Math.Sqrt(3)* coor.r);
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
