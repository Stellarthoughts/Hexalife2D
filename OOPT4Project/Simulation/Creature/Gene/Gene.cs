using System;
using System.Collections.Generic;

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

		public static bool DetermineMale => SimulationModel.Generator.NextDouble() < SimulationModel.CreatureChanceToBeMale;

		public static List<GeneType> UseGenes { get; set; } = new List<GeneType>()
		{
			GeneType.Size,
			GeneType.MetabolismSpeed,
			GeneType.Awareness,
			GeneType.ReproduceRate,
			GeneType.Carnivorousness,
		};

		public static Gene CreateChild(Gene father, Gene mother)
		{
			return new Gene(CrossGenoms(mother.Genom, father.Genom), DetermineMale);
		}	

		public static Gene RandomGene()
		{
			return new Gene(RandomGenom(), DetermineMale);
		}

		private static double[] CrossGenoms(double[] mother, double[] father)
		{
			double[] cross = new double[UseGenes.Count];
			for (int i = 0; i < UseGenes.Count; i++)
			{
				cross[i] = SimulationModel.Generator.NextDouble() < 0.5 ? mother[i] : father[i];
				cross[i] = MutateValue(cross[i]);
			}
			return cross;
		}
		private static double[] RandomGenom()
		{
			double[] rand = new double[UseGenes.Count];
			for (int i = 0; i < UseGenes.Count; i++)
			{
				rand[i] = RandomGenomValue();
			}
			return rand;
		}

		private static double MutateValue(double val)
		{
			Random rng = SimulationModel.Generator;
			if (rng.NextDouble() < SimulationModel.MutationChance)
				return Math.Clamp(val + (rng.Next(2) - 1) * SimulationModel.MutationRange, 0, 1);
			else
				return val;
		}

		private static double RandomGenomValue()
		{
			Random rnd = SimulationModel.Generator;
			return rnd.NextDouble();
		}

		public CreatureStats GetStats()
		{
			double size		= Genom[(int) GeneType.Size]; // 1/3
			double metabs	= Genom[(int) GeneType.MetabolismSpeed]; // 2/3
			double aware	= Genom[(int) GeneType.Awareness];
			double reprate	= Genom[(int) GeneType.ReproduceRate];
			double carniv	= Genom[(int) GeneType.Carnivorousness];

			return new CreatureStats()
			{
				Size = size,
				Health = (size + metabs) * 100,
				Carnivorousness = carniv,
				HungerRate = metabs * 10,
				ThirstRate = metabs * 10,
				Stealth = size * 2.0/3 + aware * 1.0/3,
				Strength = size * 1.0/3 + metabs * 2.0/3,
				Awareness = aware,
				ReproduceRate = reprate,
			};
		}
	}
}
