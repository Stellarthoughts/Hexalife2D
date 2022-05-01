using OOPT4Project.Extension;
using OOPT4Project.Simulation.Creature;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Simulation.Map
{
	public class MapController : ISimulated
	{
		public List<Tile> TileList { get; private set; } = new();
		public MapClimate MapClimate { get; private set; }

		private readonly SimulationModel _model;

		public MapController(SimulationModel model)
		{
			_model = model;
			MapClimate = new MapClimate(this);
		}

		public void CreateMapRandom(int resource, Dictionary<TileType, double> probs, double suddenSwitch)
		{
			ClearTiles();

			Random rnd = SimulationModel.Generator;
			resource -= 1;

			var currentType = TileTypeLogic.Types.RandomElementByWeight(probs, rnd);
			var initTile = new Tile(new Coordinate(0, 0), currentType);
			var borderTiles = new List<Tile>();

			RegisterTile(initTile);
			borderTiles.Add(initTile);

			while (resource > 0)
			{
				Tile rndTile;
				List<Tile> typedBorders = GetTiles(borderTiles, currentType);

				if (typedBorders.Count == 0 || rnd.NextDouble() < suddenSwitch)
				{
					rndTile = GetRandomTile(borderTiles);
				}
				else
				{
					rndTile = GetRandomTile(typedBorders);
				}

				var emptyNeighboors = GetEmptyNeighboors(TileList, rndTile);
				Coordinate crd = emptyNeighboors.PickRandom(rnd);
				Tile add = new Tile(crd, currentType);

				RegisterTile(add);

				if (GetEmptyNeighboors(TileList, add).Count != 0)
					borderTiles.Add(add);

				foreach (Tile tile in GetNeighboorTiles(TileList, add))
				{
					if (GetEmptyNeighboors(TileList, tile).Count == 0)
						borderTiles.Remove(tile);
				}

				currentType = TileTypeLogic.Types.RandomElementByWeight(probs, rnd);
				resource--;
			}

			// Ocean!
			List<Coordinate> borders = GetEmptyBorders(TileList);
			borders.ForEach(x => TileList.Add(new Tile(x, TileType.Ocean)));

			return;
		}

		private void ClearTiles() => TileList.Clear();

		private void RegisterTile(Tile tile)
		{
			TileList.Add(tile);
			tile.TileClimate.SetMapClimate(MapClimate);
		}

		public bool RegisterCreatureImmidiately(CreatureEntity creature)
		{
			if (TileList.Contains(creature.CurrentTile))
			{
				creature.CurrentTile.CreatureList.Add(creature);
				return true;
			}
			return false;
		}

		public bool RegisterCreature(CreatureEntity creature)
		{
			if (TileList.Contains(creature.CurrentTile))
			{
				creature.CurrentTile.RegisterCreature(creature);
				return true;
			}
			return false;
		}

		public bool UnregisterCreature(CreatureEntity creature)
		{
			if (TileList.Contains(creature.CurrentTile))
			{
				creature.CurrentTile.UnregisterCreature(creature);
				return true;
			}
			return false;
		}

		public bool TransferCreature(CreatureEntity ent, Tile currentTile, Tile tile)
		{
			var neighboors = GetNeighboorTiles(TileList, currentTile);
			if (tile.CanWalkTo == false || !neighboors.Contains(tile))
				return false;

			currentTile.UnregisterCreature(ent);
			tile.RegisterCreature(ent);
			return true;
		}

		public void SimulateStep()
		{
			MapClimate.SimulateStep();

			foreach (Tile tile in TileList)
			{
				tile.SimulateStep();
			}
			foreach (Tile tile in TileList)
			{
				tile.EndStep();
			}
		}

		public static Tile GetRandomTile(List<Tile> tiles)
		{
			if (tiles.Count == 0)
				throw new Exception();
			return tiles.PickRandom(SimulationModel.Generator);
		}

		public static Tile GetRandomTile(List<Tile> tiles, TileType type, bool except = false)
		{
			if (tiles.Count == 0)
				throw new Exception();
			if (!except)
				return GetTiles(tiles, type).PickRandom(SimulationModel.Generator);
			else
				return GetTiles(tiles, type, true).PickRandom(SimulationModel.Generator);
		}

		public static List<Tile> GetBorderTiles(List<Tile> tiles)
		{
			return tiles.Where(x =>
			Coordinate.GetNeighboors(x.Coordinates)
					   .Except(tiles.Select(x => x.Coordinates)).ToList().Count != 0).ToList();
		}

		public static List<Tile> GetTiles(List<Tile> tiles, TileType type, bool except = false)
		{
			if (!except)
				return tiles.Where(x => x.Type == type).ToList();
			else
				return tiles.Where(x => x.Type != type).ToList();
		}

		public static List<Tile> GetNeighboorTiles(List<Tile> tiles, Tile tile)
		{
			if (!tiles.Contains(tile))
				throw new Exception();
			var neighboors = Coordinate.GetNeighboors(tile.Coordinates);
			return tiles.Where(x => neighboors.Contains(x.Coordinates)).ToList();
		}

		public static List<Coordinate> GetEmptyNeighboors(List<Tile> tiles, Tile tile)
		{
			var neighboorCoordinates = GetNeighboorTiles(tiles, tile)
				.Select(x => x.Coordinates)
				.ToList();
			var empty = Coordinate.GetNeighboors(tile.Coordinates)
				.Except(neighboorCoordinates)
				.ToList();

			return empty;
		}

		public static List<Coordinate> GetEmptyBorders(List<Tile> tiles)
		{
			List<Coordinate> allValid = tiles.Select(x => x.Coordinates).ToList();
			List<Coordinate> withEmptyNeightboors =
				GetBorderTiles(tiles).Select(x => x.Coordinates).ToList();

			HashSet<Coordinate> emptyBorders = new();

			foreach (Coordinate coor in withEmptyNeightboors)
			{
				foreach (Coordinate empty in Coordinate.GetNeighboors(coor))
				{
					if (!allValid.Contains(empty))
					{
						emptyBorders.Add(empty);
					}
				}
			}

			return emptyBorders.ToList();
		}
	}
}
