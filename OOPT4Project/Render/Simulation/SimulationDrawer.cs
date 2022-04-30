using Microsoft.Maui.Graphics;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Render
{
	public class SimulationDrawer
	{
		private readonly SimulationModel _simulationModel;
		private readonly double _tileSize;
		private List<Tile> _tiles;

		private TileDrawer _tileDrawer;
		private CreatureDrawer _creatureDrawer;

		private Point _offset;
		private bool _snapCamera;

		public SimulationDrawer(SimulationModel simulationModel, double tileSize)
		{
			_simulationModel = simulationModel;
			_tileSize = tileSize;
			_tiles = _simulationModel.MapController.TileList;
			_tileDrawer = new(_tiles, tileSize);
			_creatureDrawer = new(_tiles, tileSize);

			Recalculate();
		}

		public void Recalculate()
		{
			_tiles = _simulationModel.MapController.TileList;
			_offset = TileDrawer.AvgHexCoordinates(_tiles.Select(x => x.Coordinates).ToList(), _tileSize);
			_snapCamera = true;
		}

		public void Draw(ICanvas canvas, CanvasCamera camera)
		{
			if(_snapCamera)
			{
				camera.SetTargetPosition(_offset.X, _offset.Y);
				camera.Update();
				_snapCamera = false;
			}

			_tileDrawer.Draw(canvas, camera);
			_creatureDrawer.Draw(canvas, camera);
		}
	}
}
