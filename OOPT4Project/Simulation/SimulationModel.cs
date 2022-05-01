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

		private static int _randomSeed = 1;
		public static int RandomSeed
		{
			get
			{
				return _randomSeed;
			}
			set
			{
				_randomSeed = value;
				Generator = new Random(_randomSeed);
			}
		}

		public static double CreatureChanceToBeMale { get; set; } = 0.5;
		public static double MutationChance { get; set; } = 0.2;
		public static double MutationRange { get; set; } = 0.3;

		// Simaltion service entities

		public static Random Generator { get; set; } = null!;

		public event EventHandler<CreatureEventArgs> CreatureDeath = null!;
		public event EventHandler<CreatureEventArgs> CreatureBorn = null!;

		public SimulationModel()
		{
			MapController = new MapController(this);
			Generator = new Random(RandomSeed);
		}

		public void Init()
		{
			MapController.Init();
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
					MapController.GetRandomTile(MapController.TileList, TileType.Grass));

				if (!MapController.RegisterCreatureImmidiately(ent))
					throw new Exception("Creature registration failed!");
			}
		}

		public List<Tile> NeighboorTiles(CreatureEntity creature)
		{
			return MapController.GetTiles(
				MapController.GetNeighboorTiles(MapController.TileList, creature.CurrentTile),
				TileType.Ocean, true);
		}

		public bool MoveTo(CreatureEntity creature, Tile tile)
		{
			return MapController.TransferCreature(creature, creature.CurrentTile, tile);
		}

		public void SimulateStep()
		{
			MapController.SimulateStep();
		}

		public bool NotifyDeath(CreatureEntity creature)
		{
			bool death = MapController.UnregisterCreature(creature);
			if (death)
				CreatureDeath.Invoke(this, new CreatureEventArgs(creature));
			return death;
		}

		public bool NotifyBorn(CreatureEntity creature)
		{
			bool born = MapController.RegisterCreature(creature);
			if (born)
				CreatureBorn.Invoke(this, new CreatureEventArgs(creature));
			return born;
		}
	}
}
