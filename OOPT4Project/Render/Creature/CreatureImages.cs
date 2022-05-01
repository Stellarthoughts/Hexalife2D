using OOPT4Project.Simulation.Creature;
using SkiaSharp;
using System.Collections.Generic;
using System.Reflection;

namespace OOPT4Project.Render
{
	public static class CreatureImages
	{
		public static readonly Dictionary<CreatureType, SKBitmap> CreatureTypeToImage = new()
		{
			{ CreatureType.Bear, FromStream("bear.png") },
			{ CreatureType.Wolf, FromStream("wolf.png") },
			{ CreatureType.Sheep, FromStream("sheep.png") },
			{ CreatureType.Mice, FromStream("mice.png") },
			{ CreatureType.Snake, FromStream("snake.png") },
			{ CreatureType.Bird, FromStream("berd.png") },
		};

		private static SKBitmap FromStream(string uri)
		{
			return SKBitmap.Decode(
				Assembly.GetExecutingAssembly()
				.GetManifestResourceStream("OOPT4Project.Resources.Images." + uri));
		}
	}
}
