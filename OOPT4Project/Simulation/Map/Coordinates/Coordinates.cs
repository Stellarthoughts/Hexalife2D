using System;

namespace OOPT4Project.Simulation.Map
{
	public class Coordinates
	{
		public int q { get; private set; }
		public int r { get; private set; }
		public int s { get; private set; }

		private static Coordinates[] _directions = new Coordinates[6] {
				Cube(1, 0, -1), Cube(1, -1, 0), Cube(0, -1, +1),
				Cube(-1, 0, 1), Cube(-1, 1, 0), Cube(0, 1, -1)
		};

		private bool _obstructed = false;

		public Coordinates(int q, int r, int s, bool obstructed = false)
		{
			this.q = q;
			this.r = r;
			this.s = s;
			_obstructed = obstructed;
		}

		public Coordinates(int q, int r, bool obstructed = false)
		{
			this.q = q;
			this.r = r;
			this.s = -(q + r);
		}

		public static Coordinates Cube(int q, int r, int s) => new Coordinates(q, r, s);
		public static Coordinates Add(Coordinates a, Coordinates b)
			=> new Coordinates(a.q + b.q, a.r + b.r, a.s + b.s);
		public static Coordinates Subtract(Coordinates a, Coordinates b) => new Coordinates(a.q - b.q, a.r - b.r, a.s - b.s);
		public static Coordinates GetDirection(int i) => _directions[i];
		public static Coordinates GetNeighboor(Coordinates coor, int dir) => Add(coor, GetDirection(dir));

		public static int GetDistance(Coordinates a, Coordinates b)
		{
			Coordinates vec = Subtract(a, b);
			return (Math.Abs(vec.q) + Math.Abs(vec.r) + Math.Abs(vec.s)) / 2;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || !(obj is Coordinates))
				return false;
			else
			{
				Coordinates coor = (Coordinates)obj;
				return coor.q == q && coor.r == r;
			}
				
		}
	}
}
