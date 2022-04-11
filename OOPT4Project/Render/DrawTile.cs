using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Map;
using System;

namespace OOPT4Project.Render
{
    public class DrawTile
    {
        public static Point HexToPixel(Coordinates coor, double size)
        {
            double x = size * (Math.Sqrt(3) * coor.q + Math.Sqrt(3)/2 * coor.r);
            double y = size * (3.0/2 * coor.r);
            return new Point(x, y);
        }

        public static Coordinates PixelToHex(Point point, double size)
        {
            double q = (2.0/ 3 * point.X) / size;
            double r = (-1.0/ 3 * point.X + Math.Sqrt(3) / 3 * point.Y) / size;

            int q_r = (int) Math.Round(q);
            int r_r = (int) Math.Round(r);

            return new Coordinates(0,0);
        }

    }
}
