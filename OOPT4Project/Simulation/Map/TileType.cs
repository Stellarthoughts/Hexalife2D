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
		public static readonly List<TileType> Types = new List<TileType>() {
			TileType.Grass,
			TileType.Lake,
			TileType.Hills,
			TileType.Marsh,
			TileType.Desert,
			TileType.Badland,
			TileType.Ocean
		};
	}
}
