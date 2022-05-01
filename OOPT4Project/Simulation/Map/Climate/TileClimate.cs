using System;

namespace OOPT4Project.Simulation.Map
{
	public class TileClimate : ISimulated
	{
		private MapClimate _mapClimate = null!;

		public double WaterFactor { get; private set; }
		public double FoodFactor { get; private set; }
		public double BirthFactor { get; private set; }
		public bool RandomBirth { get; private set; }

		private static readonly double _factorVarience = 0.01;

		public TileClimate() { }

		public void SimulateStep()
		{
			Random gen = SimulationModel.Generator;
			WaterFactor += (gen.NextDouble() * 2 - 1) * _factorVarience;
			FoodFactor += (gen.NextDouble() * 2 - 1) * _factorVarience;
		}

		public void SetMapClimate(MapClimate mapClimate)
		{
			if (_mapClimate != null)
				mapClimate.WeatherChange -= MapClimate_WeatherChange;

			_mapClimate = mapClimate;
			mapClimate.WeatherChange += MapClimate_WeatherChange;
		}

		private void MapClimate_WeatherChange(object? sender, WeatherChangeEventArgs e)
		{
			WaterFactor = e.WaterFactor;
			FoodFactor = e.FoodFactor;
			BirthFactor = e.RandomBirthFactor;
			RandomBirth = e.RandomBirth;
		}
	}
}
