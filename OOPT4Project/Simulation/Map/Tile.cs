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

		private double _foodResource;
		private double _waterResource;

		private double _foodReplenish;
		private double _waterReplenish;

		public Tile(Coordinates coor, TileType type)
		{
			TileClimate = new TileClimate();
			Coordinates = coor;
			Type = type;
			TileTypeLogic.Resources.TryGetValue(type, out TileTypeResources resources);
			_foodResource = resources.InitialFood;
			_waterResource = resources.InitialWater;
			_foodReplenish = resources.ReplenishRateFood;
			_waterReplenish = resources.ReplenishRateWater;
		}

		public void SimulateStep()
		{
			throw new NotImplementedException();
		}
	}
}