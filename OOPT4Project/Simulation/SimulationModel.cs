using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation
{
	public class SimulationModel : ISimulated
	{
		public MapController MapController { get; private set; }

		// Simulation params
		public static int RandomSeed { get; set; } = 19;
		public static double CreatureChanceToBeMale { get; set; } = 0.5;
		public static double MutationChance { get; set; } = 0.04;
		public static double MutationRange { get; set; } = 0.02;

		// Simaltion service entities

		public static Random Generator { get; set; } = new Random(RandomSeed);

		public SimulationModel()
		{
			MapController = new MapController(this);
		}

		public void CreateMapRandom(int resource, Dictionary<TileType, double> probs, double suddenSwitch)
		{
			MapController.CreateMapRandom(resource, probs, suddenSwitch);
		}

		public void PopulateSimulation(int count)
		{
			for (int i = 0; i < count; i++)
			{
				CreatureEntity ent = new CreatureEntity(this, Gene.RandomGene(),
					MapController.GetRandomTile(MapController.TileList, TileType.Ocean, true));
				
				if (!MapController.RegisterCreature(ent, ent.CurrentTile))
					throw new Exception("Creature registration failed!");
			}
		}

		public List<Tile> NeighboorTiles(CreatureEntity ent)
		{
			return MapController.GetTiles(
				MapController.GetNeighboorTiles(MapController.TileList, ent.CurrentTile), 
				TileType.Ocean, true);
		}

		public bool MoveTo(CreatureEntity ent, Tile tile)
		{
			return MapController.TransferCreature(ent, ent.CurrentTile, tile);
		}

		public void SimulateStep()
		{
			MapController.SimulateStep();
		}
	}
}
