using System;

namespace OOPT4Project.Simulation.Creature
{
	public class Gene
	{
		public bool IsMale { get; private set; }

		public double[] Genom { get; private set; }

		public Gene(double[] genom, bool isMale)
		{
			Genom = genom;
			IsMale = isMale;
		}

		public static Gene CreateChild(Gene father, Gene mother)
		{
			bool male = SimulationModel.Generator.NextDouble() > SimulationModel.CreatureChanceToBeMale;
			return new Gene(father.Genom, male);
		}

		public static Gene RandomGene()
		{
			throw new NotImplementedException();
		}
	}
}
