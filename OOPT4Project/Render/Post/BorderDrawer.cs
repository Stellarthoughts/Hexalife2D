using Microsoft.Maui.Graphics;
using System;

namespace OOPT4Project.Render
{
	public static class BorderDrawer
	{
		public static void DrawHexagonalBorder(ICanvas canvas, Color color, PointF top, PointF bottom, double size)
		{
			double x = top.X;
			double y = top.Y;

			canvas.FillColor = color;
			canvas.StrokeColor = color;
			canvas.StrokeSize = 1;

			double sizeIncrement = size * Math.Sqrt(3) * 3 / 4;

			for (double i = y; i <= bottom.Y + sizeIncrement; i += sizeIncrement)
			{
				PathF path = TileDrawer.PathTile(new Point(x, i), new Simulation.Map.Coordinates(0, 0), size);
				canvas.FillPath(path);
				canvas.DrawPath(path);
			}
		}
	}
}
