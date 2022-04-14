using OOPT4Project.Simulation.Creature;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class Tile : ISimulated
	{
		public Coordinates Coordinates { get; private set; }
		public TileClimate TileClimate { get; private set; }
		public List<CreatureEntity> Creatures { get; private set; } = new List<CreatureEntity>();
		public TileType Type { get; private set; }

		private double _foorResource;
		private double _waterResource;

		public Tile(Coordinates coor, TileType type)
		{
			TileClimate = new TileClimate();
			Coordinates = coor;
			Type = type;
		}

		public List<Tile> GetNeighboorList()
		{
			return new List<Tile>();
		}

		public void SimulateStep()
		{
			throw new NotImplementedException();
		}
	}
}