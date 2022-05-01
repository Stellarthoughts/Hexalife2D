using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Map
{
	public class WeatherChangeEventArgs
	{
		public WeatherChangeEventArgs(double foodFactor, double waterFactor, bool randomBirth, double randomBirthFactor)
		{
			FoodFactor = foodFactor;
			WaterFactor = waterFactor;
			RandomBirth = randomBirth;
			RandomBirthFactor = randomBirthFactor;
		}

		public double FoodFactor { get; private set; }
		public double WaterFactor { get; private set; }
		public bool RandomBirth { get; private set; }
		public double RandomBirthFactor { get; private set; }
	}
}
