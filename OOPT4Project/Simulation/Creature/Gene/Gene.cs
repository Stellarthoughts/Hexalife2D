using System;

namespace OOPT4Project.Simulation.Creature
{
	public class Gene
	{
		public bool IsMale { get; private set; }

		public double[] Genom { get; private set; }

		public Gene(bool isMale)
		{
			Genom = new double[0];
			IsMale = isMale;
		}

		public static Gene CreateChild(Gene father, Gene mother)
		{
			bool male = SimulationModel.Generator.NextDouble() > SimulationModel.CreatureChanceToBeMale;
			return new Gene(male);
		}

		public static Gene RandomGene()
		{
			throw new NotImplementedException();
		}

		public Stats CreateStats()
		{
			Stats stats = new Stats();
			return stats;
		}
	}
}
