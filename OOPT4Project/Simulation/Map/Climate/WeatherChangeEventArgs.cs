namespace OOPT4Project.Simulation.Map
{
	public class WeatherChangeEventArgs
	{
		public WeatherChangeEventArgs(ClimateFactors factors)
		{
			FoodFactor = factors.FoodFactor;
			WaterFactor = factors.WaterFactor;
			RandomBirth = factors.RandomBirth;
			RandomBirthFactor = factors.RandomBirthFactor;
		}

		public double FoodFactor { get; private set; }
		public double WaterFactor { get; private set; }
		public bool RandomBirth { get; private set; }
		public double RandomBirthFactor { get; private set; }
	}
}
