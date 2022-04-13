using System;

namespace OOPT4Project.Simulation.Map
{
	public class MapClimate : ISimulated
	{
		private MapController _controller;
		// TODO: Event climate change

		public MapClimate(MapController controller)
		{
			_controller = controller;
		}
		public void SimulateStep()
		{
			throw new NotImplementedException();
		}
	}
}
