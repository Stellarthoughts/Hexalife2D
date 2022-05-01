using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace OOPT4Project.Render
{
	public class SimulationDrawer
	{
		private readonly SimulationModel _simulationModel;
		private readonly float _tileSize;
		private List<Tile> _tiles;

		private readonly TileDrawer _tileDrawer;
		private readonly CreatureDrawer _creatureDrawer;

		private SKPoint _offset;
		private bool _snapCamera;

		public SimulationDrawer(SimulationModel simulationModel, float tileSize)
		{
			_simulationModel = simulationModel;
			_tileSize = tileSize;
			_tiles = _simulationModel.MapController.TileList;
			_tileDrawer = new(_tiles, tileSize);
			_creatureDrawer = new(_tiles, tileSize);

			Recalculate();
		}

		public void SelectTile(Tile tile)
		{
			_tileDrawer.SelectTile(tile);
		}

		public void Recalculate()
		{
			_tiles = _simulationModel.MapController.TileList;
			_offset = TileDrawer.AvgHexCoordinates(_tiles.Select(x => x.Coordinates).ToList(), _tileSize);
			_snapCamera = true;
		}

		public void Draw(SKCanvas canvas, CanvasCamera camera)
		{
			if (_snapCamera)
			{
				camera.SetTargetPosition(_offset.X, _offset.Y);
				camera.Update();
				_snapCamera = false;
			}

			_tileDrawer.Draw(canvas, camera);
			_creatureDrawer.Draw(canvas, camera);
		}

		public Tile? GetTileFromPixel(Point point)
		{
			return _tileDrawer.GetTileFromPixel(point);
		}
	}
}
