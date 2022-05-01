using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Map
{
	public static class ClimateTypeLogic
	{
		public static ClimateType Cycle(ClimateType type)
		{
			switch(type)
			{
				case ClimateType.Summer: return ClimateType.Fall;
				case ClimateType.Fall:	 return ClimateType.Winter;
				case ClimateType.Winter: return ClimateType.Spring;
				case ClimateType.Spring: return ClimateType.Summer;
			}
			return ClimateType.Summer;
		}

		public static ClimateType StrangeCycle(ClimateType type)
		{
			switch (type)
			{
				case ClimateType.Summer:	return ClimateType.Hellscape;
				case ClimateType.Fall:		return ClimateType.Blessed;
				case ClimateType.Winter:	return ClimateType.Blessed;
				case ClimateType.Spring:	return ClimateType.Hellscape;
				case ClimateType.Hellscape: return ClimateType.Blessed;
				case ClimateType.Blessed:	return ClimateType.Hellscape;
			}
			return ClimateType.Summer;
		}
	}
}
