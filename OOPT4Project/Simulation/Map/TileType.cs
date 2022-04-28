using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public enum TileType
	{
		Grass,
		Lake,
		Hills,
		Marsh,
		Desert,
		Badland,
		Ocean
	}

	public struct TileTypeResources
	{
		public double InitialFood { get; private set; }
		public double InitialWater { get; private set; }
		public double ReplenishRateFood { get; private set; }

		public double ReplenishRateWater { get; private set; }

		public TileTypeResources(double initialFood, double initialWater, double replenishRateFood, double replenishRateWater)
		{
			InitialFood = initialFood;
			InitialWater = initialWater;
			ReplenishRateFood = replenishRateFood;
			ReplenishRateWater = replenishRateWater;
		}
	}

	public static class TileTypeLogic
	{
		public static readonly List<TileType> Types = new() {
			TileType.Grass,
			TileType.Lake,
			TileType.Hills,
			TileType.Marsh,
			TileType.Desert,
			TileType.Badland,
			TileType.Ocean
		};

		public static readonly Dictionary<TileType, double> ProbWeightsDefault = new()
		{
			{ TileType.Grass,	1 },
			{ TileType.Lake,	0.2 },
			{ TileType.Hills,	0.2 },
			{ TileType.Desert,	0.2 },
			{ TileType.Badland, 0.3 },
			{ TileType.Marsh,	0.3 },
			{ TileType.Ocean,	0 }
		};

		public static readonly Dictionary<TileType, bool> CanWalkTo = new()
		{
			{ TileType.Grass, true },
			{ TileType.Lake, true },
			{ TileType.Hills, true },
			{ TileType.Desert, true },
			{ TileType.Badland, true },
			{ TileType.Marsh, true },
			{ TileType.Ocean, false }
		};

		public static readonly Dictionary<TileType, TileTypeResources> Resources = new()
		{
			{ TileType.Grass,	new TileTypeResources(50, 30, 5, 3) },
			{ TileType.Lake,    new TileTypeResources(10, 50, 1, 5) },
			{ TileType.Hills,   new TileTypeResources(10, 20, 1, 2) },
			{ TileType.Desert,  new TileTypeResources(10, 10, 1, 1) },
			{ TileType.Badland, new TileTypeResources(15, 15, 1.5, 1.5) },
			{ TileType.Marsh,   new TileTypeResources(20, 10, 2, 1) },
			{ TileType.Ocean,   new TileTypeResources(0, 0, 0, 0) }
		};
	}
}
