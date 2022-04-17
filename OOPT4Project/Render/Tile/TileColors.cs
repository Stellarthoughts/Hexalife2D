using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation.Map;
using System.Collections.Generic;

namespace OOPT4Project.Render
{
	public static class TileColors
	{
		public static readonly Dictionary<TileType, Color> TileTypeToColor = new()
		{
			{ TileType.Grass,  Color.FromArgb("51C65C") },
			{ TileType.Lake,   Color.FromArgb("27D8FF") },
			{ TileType.Hills,  Color.FromArgb("87938C") },
			{ TileType.Desert, Color.FromArgb("C1BC40") },
			{ TileType.Badland,Color.FromArgb("7B6748") },
			{ TileType.Marsh,  Color.FromArgb("47844D") },
			{ TileType.Ocean,  Color.FromArgb("5979EB") }
		};
	}
}
