using OOPT4Project.Simulation.Creature;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation
{
	public class SimulationModel : ISimulated
	{
		public List<CreatureEntity> CreatureList { get; private set; }
		public MapController MapController { get; private set; }

		// Simulation params
		public static int RandomSeed { get; set; } = 1;
		public static double CreatureChanceToBeMale { get; set; } = 0.5;

		// Simaltion service entities

		public static Random Generator { get; set; } = new Random(RandomSeed);

		// Genome defaults
		public static double MetabolismSpeed { get; set; } = 1;

		public SimulationModel()
		{
			CreatureList = new List<CreatureEntity>();
			MapController = new MapController(this);
		}

		public void CreateMap()
		{
			MapController.CreateMap();
		}

		public void PopulateSimulation(int count)
		{
			CreatureList.Clear();
			for (int i = 0; i < count; i++)
				CreatureList.Add(new CreatureEntity(Gene.RandomGene(), MapController.GetRandomTile()));
		}

		public void SimulateStep()
		{
			throw new NotImplementedException();
		}
	}
}
