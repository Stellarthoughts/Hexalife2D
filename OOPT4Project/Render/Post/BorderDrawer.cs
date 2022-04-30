using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Map;
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
			int j = 0;

			for (double i = 0; i <= bottom.Y + sizeIncrement; i += sizeIncrement)
			{
				PathF path = TileDrawer.PathTile(new Coordinates(0, j), size);
				path.Move(top.X, top.Y);
				canvas.FillPath(path);
				canvas.DrawPath(path);
				j++;
			}
		}
	}
}
