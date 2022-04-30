using OOPT4Project.Simulation.Map;
using SkiaSharp;
using System.Collections.Generic;

namespace OOPT4Project.Render
{
	public static class TileColors
	{
		public static readonly Dictionary<TileType, SKColor> TileTypeToColor = new()
		{
			{ TileType.Grass, SKColor.Parse("51C65C") },
			{ TileType.Lake, SKColor.Parse("27D8FF") },
			{ TileType.Hills, SKColor.Parse("87938C") },
			{ TileType.Desert, SKColor.Parse("C1BC40") },
			{ TileType.Badland, SKColor.Parse("7B6748") },
			{ TileType.Marsh, SKColor.Parse("47844D") },
			{ TileType.Ocean, SKColor.Parse("5979EB") }
		};
	}
}
