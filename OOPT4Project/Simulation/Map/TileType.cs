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
			{ TileType.Grass, 1 },
			{ TileType.Lake, 0.2 },
			{ TileType.Hills, 0.2 },
			{ TileType.Desert, 0.2 },
			{ TileType.Badland, 0.3 },
			{ TileType.Marsh, 0.3 },
			{ TileType.Ocean, 0 }
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
	}
}
