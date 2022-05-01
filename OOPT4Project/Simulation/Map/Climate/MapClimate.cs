using System;

namespace OOPT4Project.Simulation.Map
{
	public class MapClimate : ISimulated
	{
		public ClimateType ClimateType { get; private set; }

		private readonly MapController _controller;

		private double _climateChangeChance = 0.03;
		
		public event EventHandler<WeatherChangeEventArgs> WeatherChange = null!;

		public MapClimate(MapController controller)
		{
			_controller = controller;
			ClimateType = ClimateType.Summer;
		}
		public void SimulateStep()
		{
			if(SimulationModel.Generator.NextDouble() < _climateChangeChance)
			{

			}
		}
	}
}
