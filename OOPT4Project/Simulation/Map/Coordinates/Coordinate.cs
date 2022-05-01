using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class Coordinate
	{
		public int q { get; private set; }
		public int r { get; private set; }
		public int s { get; private set; }

		private static readonly Coordinate[] _directions = new Coordinate[6] {
				Cube(1, 0, -1), Cube(1, -1, 0), Cube(0, -1, +1),
				Cube(-1, 0, 1), Cube(-1, 1, 0), Cube(0, 1, -1)
		};

		public Coordinate(int q, int r, int s)
		{
			this.q = q;
			this.r = r;
			this.s = s;
		}

		public Coordinate(int q, int r)
		{
			this.q = q;
			this.r = r;
			this.s = -(q + r);
		}

		public static Coordinate Cube(int q, int r, int s) => new Coordinate(q, r, s);
		public static Coordinate Add(Coordinate a, Coordinate b)
			=> new Coordinate(a.q + b.q, a.r + b.r, a.s + b.s);
		public static Coordinate Subtract(Coordinate a, Coordinate b) => new Coordinate(a.q - b.q, a.r - b.r, a.s - b.s);
		public static Coordinate GetDirection(int i) => _directions[i];
		public static Coordinate GetNeighboor(Coordinate coor, int dir) => Add(coor, GetDirection(dir));
		public static List<Coordinate> GetNeighboors(Coordinate coor)
		{
			List<Coordinate> neighbooring = new();
			for (int i = 0; i < _directions.Length; i++)
			{
				neighbooring.Add(GetNeighboor(coor, i));
			}
			return neighbooring;
		}

		public static int GetDistance(Coordinate a, Coordinate b)
		{
			Coordinate vec = Subtract(a, b);
			return (Math.Abs(vec.q) + Math.Abs(vec.r) + Math.Abs(vec.s)) / 2;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null || !(obj is Coordinate))
				return false;
			else
			{
				Coordinate coor = (Coordinate)obj;
				return coor.q == q && coor.r == r;
			}

		}
		public override int GetHashCode()
		{
			return q ^ r;
		}
	}

	public class CoordinatesEqualityComparer : IEqualityComparer<Coordinate>
	{
		public bool Equals(Coordinate? c1, Coordinate? c2)
		{
			if (c1 == null || c2 == null)
				return false;
			return c1.q == c2.q && c1.r == c2.r;
		}
		public int GetHashCode(Coordinate c)
		{
			return c.q ^ c.r;
		}
	}
}
