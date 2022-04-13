using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public struct MapSettings
	{
		public int qLeft;
		public int qRight;
		public int rLeft;
		public int rRight;
	}
	public class MapController : ISimulated
	{
		public List<Tile> Tiles { get; private set; } = new List<Tile>();
		public MapClimate Climate { get; private set; }

		private SimulationModel _model;

		public MapController(SimulationModel model)
		{ 
			_model = model;
			Climate = new MapClimate(this);
			//CreateMap(new MapSettings() { qLeft = -3, qRight = 3, rLeft = -5, rRight = 5});
			CreateMapRandom(50);
		}

		public void CreateMap(MapSettings set)
		{
			for(int i = set.qLeft; i <= set.qRight; i++)
			{	
				for(int j = set.rLeft; j <= set.rRight; j++)
				{
					Tiles.Add(new Tile(new Coordinates(i, j), TileType.Flat));
				}
			}
		}

		public void CreateMapRandom(int resource)
		{
			Tiles.Add(new Tile(new Coordinates(0,0), TileType.Flat));
			Random rnd = SimulationModel.Generator;
			resource -= 1;
			while(resource > 0)
			{
				Tile rndTile = Tiles[rnd.Next(Tiles.Count)];
				Coordinates crd = Coordinates.Add(rndTile.Coordinates, Coordinates.GetDirection(rnd.Next(6)));
				if (Tiles.FindIndex(x => x.Coordinates.Equals(crd)) > 0)
					continue;
				Tiles.Add(new Tile(crd, TileType.Flat));
				resource--;
			}
			return;
		}

		public void SimulateStep()
		{
			Climate.SimulateStep();
			foreach (Tile tile in Tiles) tile.SimulateStep();
		}

		internal Tile GetRandomTile()
		{
			throw new NotImplementedException();
		}
	}
}
