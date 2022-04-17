using System;
using System.Linq;
using System.Collections.Generic;
using OOPT4Project.Extension;

namespace OOPT4Project.Simulation.Map
{
	public class MapController : ISimulated
	{
		public List<Tile> TileList { get; private set; } = new List<Tile>();
		public MapClimate MapClimate { get; private set; }

		private SimulationModel _model;

		public MapController(SimulationModel model)
		{ 
			_model = model;
			MapClimate = new MapClimate(this);

			Dictionary<TileType, double> probs = new()
			{
				{ TileType.Grass, 1 },
				{ TileType.Lake, 0.2 },
				{ TileType.Hills, 0.2 },
				{ TileType.Desert, 0.2 },
				{ TileType.Badland, 0.3 },
				{ TileType.Marsh, 0.3 },
				{ TileType.Ocean, 0 }
			};

			CreateMapRandom(100, probs);
		}

		public void CreateMapRandom(int resource, Dictionary<TileType, double> probs)
		{
			TileList.Clear();

			Random rnd = SimulationModel.Generator;
			resource -= 1;

			var currentType = TileTypeLogic.Types.RandomElementByWeight(probs, rnd);
			var initTile = new Tile(new Coordinates(0, 0), currentType);

			TileList.Add(initTile);

			int giveUp = 0;
			while (resource > 0)
			{
				Tile rndTile;
				if (GetTiles(currentType).Count == 0 || giveUp > 10)
				{
					rndTile = GetRandomTile();
				}
				else
					rndTile = GetRandomTile(currentType);

				var emptyNeighboors = GetEmptyNeighboors(rndTile);

				if (emptyNeighboors.Count == 0)
				{
					giveUp++;
					continue;
				}
				else
					giveUp = 0;

				Coordinates crd = emptyNeighboors.PickRandom();

				TileList.Add(new Tile(crd, currentType));
				
				currentType = TileTypeLogic.Types.RandomElementByWeight(probs, rnd);
				resource--;
			}

			List<Coordinates> borders = GetEmptyBorders();
			borders.ForEach((x) => TileList.Add(new Tile(x, TileType.Ocean)));

			return;
		}

		public void SimulateStep()
		{
			MapClimate.SimulateStep();
			foreach (Tile tile in TileList) tile.SimulateStep();
		}

		public Tile GetRandomTile()
		{
			if (TileList.Count == 0)
				throw new Exception();
			return TileList[SimulationModel.Generator.Next(TileList.Count)];
		}

		public List<Tile> GetTiles(TileType type)
		{
			return TileList.Where((x) => x.Type == type).ToList();
		}

		public Tile GetRandomTile(TileType type)
		{
			if (TileList.Count == 0)
				throw new Exception();
			return GetTiles(type).PickRandom();
		}

		public List<Tile> GetNeighboors(Tile tile)
		{
			if (TileList.IndexOf(tile) == -1)
				throw new Exception();
			var neighboors = Coordinates.GetNeighboors(tile.Coordinates);
			return TileList.Where((x) => neighboors.Contains(x.Coordinates)).ToList();
		}

		public List<Coordinates> GetEmptyNeighboors(Tile tile)
		{
			var neighboors = Coordinates.GetNeighboors(tile.Coordinates);
			var neighboorTiles = GetNeighboors(tile);
			var neighboorCoordinates = neighboorTiles.Select((x) => x.Coordinates).ToList();
			var empty = neighboors.Except(neighboorCoordinates).ToList();

			return empty;
		}

		public List<Coordinates> GetEmptyBorders()
		{
			List<Coordinates> allValid = TileList.Select((x) => x.Coordinates).ToList();
			List<Coordinates> withEmptyNeightboors = 
				TileList.Where((x) => Coordinates.GetNeighboors(x.Coordinates).Count != 0).Select((x) => x.Coordinates).ToList();

			HashSet<Coordinates> emptyBorders = new();

			foreach(Coordinates coor in withEmptyNeightboors)
			{
				foreach(Coordinates empty in Coordinates.GetNeighboors(coor))
				{
					if(!allValid.Contains(empty))
					{
						emptyBorders.Add(empty);
					}
				}
			}

			return emptyBorders.ToList();
		}
	}
}
