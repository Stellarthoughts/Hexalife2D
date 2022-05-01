using OOPT4Project.Simulation.Map;
using SkiaSharp;
using System.Collections.Generic;

namespace OOPT4Project.Render
{
	public static class ClimateVisual
	{
		public static readonly Dictionary<ClimateType, SKColor> ClimateTypeToColorMask = new()
		{
			{ ClimateType.Summer, SKColor.Empty },
			{ ClimateType.Fall, SKColor.Parse("#eb7310") },
			{ ClimateType.Winter, SKColor.Parse("#16bbe0") },
			{ ClimateType.Spring, SKColor.Parse("#e890e2") },
			{ ClimateType.Hellscape, SKColor.Parse("#ff1d00") },
			{ ClimateType.Blessed, SKColor.Parse("#98f5c2") },
		};

	}
}
