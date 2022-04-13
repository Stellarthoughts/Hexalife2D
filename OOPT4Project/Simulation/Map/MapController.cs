using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class MapController : ISimulated
	{
		public List<Tile> Tiles { get; private set; } = new List<Tile>();
		private SimulationModel _model;

		public MapController(SimulationModel model)
		{
			_model = model;
		}

		public void CreateMap()
		{

		}

		public void SimulateStep()
		{
			throw new NotImplementedException();
		}

		internal Tile GetRandomTile()
		{
			throw new NotImplementedException();
		}
	}
}
