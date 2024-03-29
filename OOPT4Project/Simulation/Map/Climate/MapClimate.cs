﻿using System;

namespace OOPT4Project.Simulation.Map
{
	public class MapClimate : ISimulated
	{
		public ClimateType ClimateType { get; private set; }

		private readonly double _strangeCycleChance = 0.1;

		private int _currentCycleLength = 0;

		public static readonly int CycleMaxLength = 225;

		public event EventHandler<WeatherChangeEventArgs> WeatherChange = null!;

		public MapClimate()
		{
			ClimateType = ClimateType.Summer;
		}

		public void Reset()
		{
			ClimateType = ClimateType.Summer;
			_currentCycleLength = 0;
			UpdateFactors();
		}

		private void UpdateFactors()
		{
			ClimateTypeLogic.ClimateTypeToFactors.TryGetValue(ClimateType, out var factors);
			WeatherChange.Invoke(this, new WeatherChangeEventArgs(factors));
		}

		public void SimulateStep()
		{
			_currentCycleLength++;
			if (_currentCycleLength >= CycleMaxLength)
			{
				ChangeCycle();
			}
		}

		private void ChangeCycle()
		{
			Random gen = SimulationModel.Generator;
			if (gen.NextDouble() < _strangeCycleChance)
				ClimateType = ClimateTypeLogic.StrangeCycle(ClimateType);
			else
				ClimateType = ClimateTypeLogic.Cycle(ClimateType);

			_currentCycleLength = 0;
			UpdateFactors();
		}
	}
}
