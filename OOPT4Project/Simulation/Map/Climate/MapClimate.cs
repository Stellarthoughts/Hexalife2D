using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class MapClimate : ISimulated
	{
		private MapController _controller;
		private List<TileClimate> _tileClimateList = new();
		// TODO: Event climate change

		public MapClimate(MapController controller)
		{
			_controller = controller;
			_controller.TileList.ForEach(x => _tileClimateList.Add(x.TileClimate));
		}
		public void SimulateStep()
		{
			throw new NotImplementedException();
		}
	}
}
