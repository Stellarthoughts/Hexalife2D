using System;

namespace OOPT4Project.Simulation.Map
{
	public class TileClimate : ISimulated
	{
		private MapClimate _mapClimate = null!;

		private double _waterFactor;
		private double _foodFactor;
		private double _birthFactor;
		private bool _randomBirth;

		public TileClimate() {}

		public void SimulateStep()
		{
			
		}

		public void SetMapClimate(MapClimate mapClimate)
		{
			mapClimate.WeatherChange += MapClimate_WeatherChange;
		}

		private void MapClimate_WeatherChange(object? sender, WeatherChangeEventArgs e)
		{
			_waterFactor = e.WaterFactor;
			_foodFactor = e.FoodFactor;
			_birthFactor = e.RandomBirthFactor;
			_randomBirth = e.RandomBirth;
		}
	}
}
