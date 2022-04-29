namespace OOPT4Project.Simulation.Map
{
	public class WeatherChangeArgs
	{

	}

	public class MapClimate : ISimulated
	{
		private MapController _controller;

		public delegate void WeatherChangeDelegate(object sender, WeatherChangeArgs e);
		public event WeatherChangeDelegate? WeatherChange;

		public MapClimate(MapController controller)
		{
			_controller = controller;
		}
		public void SimulateStep()
		{

		}
	}
}
