using OOPT4Project.Simulation.Creature;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class Tile : ISimulated
	{
		public Coordinates Coordinates { get; private set; }
		public TileClimate TileClimate { get; private set; }
		public List<CreatureEntity> CreatureList { get; private set; } = new List<CreatureEntity>();
		public TileType Type { get; private set; }
		public bool CanWalkTo { get; private set; }

		private double _foodResource;
		private double _waterResource;

		private TileTypeResources _resources;

		public Tile(Coordinates coor, TileType type)
		{
			TileClimate = new TileClimate();

			Coordinates = coor;
			Type = type;

			TileTypeLogic.Resources.TryGetValue(type, out TileTypeResources resources);
			TileTypeLogic.CanWalkTo.TryGetValue(type, out bool walk);
			CanWalkTo = walk;

			_resources = resources;
			_foodResource = resources.InitialFood;
			_waterResource = resources.InitialWater;
		}

		public void SimulateStep()
		{
			throw new NotImplementedException();
		}
	}
}