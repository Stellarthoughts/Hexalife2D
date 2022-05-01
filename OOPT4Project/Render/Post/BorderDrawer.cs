using OOPT4Project.Simulation.Map;
using SkiaSharp;
using System;

namespace OOPT4Project.Render
{
	public static class BorderDrawer
	{
		public static void DrawHexagonalBorder(SKCanvas canvas, SKPaint paint, SKPoint top, SKPoint bottom, float size)
		{
			double x = top.X;
			double y = top.Y;

			double sizeIncrement = size * Math.Sqrt(3) * 3 / 4;
			int j = 0;

			for (double i = 0; i <= bottom.Y + sizeIncrement; i += sizeIncrement)
			{
				SKPath path = TileDrawer.PathTile(new Coordinate(0, j), size);
				path.Offset(top.X, top.Y);
				canvas.DrawPath(path, paint);
				j++;
			}
		}
	}
}
