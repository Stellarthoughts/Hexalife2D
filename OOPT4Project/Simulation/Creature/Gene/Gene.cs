using System;
using System.Collections.Generic;
using OOPT4Project.Extension;

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

		public double GetGenom(GeneType geneType) => Genom[(int)geneType];

		public double MapGenom(double genom, double from, double to) => genom.Map(0,1,from,to);

		public double GetMapGenom(GeneType geneType, double from, double to)
		{
			double val = Genom[(int)geneType];
			return MapGenom(val, from, to);
		}

		public CreatureStats GetStats()
		{
			double size		= GetMapGenom(GeneType.Size, 0.25, 0.75);
			double metabs	= GetMapGenom(GeneType.MetabolismSpeed, 0.35, 0.75);
			double aware	= GetMapGenom(GeneType.Awareness, 0.35, 0.75);
			double reprate	= GetMapGenom(GeneType.ReproduceRate, 0.1, 0.5);
			double carniv	= GetMapGenom(GeneType.Carnivorousness, 0.2, 0.8);

			return new CreatureStats()
			{
				EnergyResource = size / 3,
				HealthMax = (size * 2 + metabs * 1),
				HealingRate = metabs / 10,
				Carnivorousness = carniv,
				HungerRate = (metabs * 3 + size * 2) / 5 / 10,
				ThirstRate = (metabs * 3 + size * 2) / 5 / 10,
				Stealth = (1 - size + aware + metabs) / 3,
				Strength = (size * 1.5 + metabs * 3.5) / 5,
				Awareness = aware,
				ReproduceRate = (reprate * 2.5 + metabs * 0.5) / 3 / 8.5,
				Age = (2 - metabs * 2 + size) / 3 * 70,
			};
		}
	}
}
