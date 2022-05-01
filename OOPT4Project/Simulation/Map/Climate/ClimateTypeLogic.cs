using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public struct ClimateFactors
	{
		public ClimateFactors(double waterFactor, double foodFactor, bool randomBirth, double randomBirthFactor)
		{
			WaterFactor = waterFactor;
			FoodFactor = foodFactor;
			RandomBirth = randomBirth;
			RandomBirthFactor = randomBirthFactor;
		}
		public double WaterFactor { get; private set; }
		public double FoodFactor { get; private set; }
		public bool RandomBirth { get; private set; }
		public double RandomBirthFactor { get; private set; }
	}

	public static class ClimateTypeLogic
	{
		public static Dictionary<ClimateType, ClimateFactors> ClimateTypeToFactors = new()
		{
			{ ClimateType.Summer, new ClimateFactors(1.1, 1.1, false, 0) },
			{ ClimateType.Fall, new ClimateFactors(1.1, 0.7, false, 0) },
			{ ClimateType.Winter, new ClimateFactors(0.6, 0.6, false, 0) },
			{ ClimateType.Spring, new ClimateFactors(0.9, 1.6, false, 0) },
			{ ClimateType.Hellscape, new ClimateFactors(0.3, 0.3, true, 0.000001) },
			{ ClimateType.Blessed, new ClimateFactors(1.5, 1.5, true, 0.0001) },
		};

		public static Dictionary<ClimateType, string> ClimateTypeToNames = new()
		{
			{ ClimateType.Summer, "Summer" },
			{ ClimateType.Fall, "Fall" },
			{ ClimateType.Winter, "Winter" },
			{ ClimateType.Spring, "Spring" },
			{ ClimateType.Hellscape, "Hellscape" },
			{ ClimateType.Blessed, "Blessed" },
		};

		public static ClimateType Cycle(ClimateType type)
		{
			switch (type)
			{
				case ClimateType.Summer: return ClimateType.Fall;
				case ClimateType.Fall: return ClimateType.Winter;
				case ClimateType.Winter: return ClimateType.Spring;
				case ClimateType.Spring: return ClimateType.Summer;
			}
			return ClimateType.Summer;
		}

		public static ClimateType StrangeCycle(ClimateType type)
		{
			switch (type)
			{
				case ClimateType.Summer: return ClimateType.Hellscape;
				case ClimateType.Fall: return ClimateType.Blessed;
				case ClimateType.Winter: return ClimateType.Blessed;
				case ClimateType.Spring: return ClimateType.Hellscape;
				case ClimateType.Hellscape: return ClimateType.Blessed;
				case ClimateType.Blessed: return ClimateType.Hellscape;
			}
			return ClimateType.Summer;
		}
	}
}
