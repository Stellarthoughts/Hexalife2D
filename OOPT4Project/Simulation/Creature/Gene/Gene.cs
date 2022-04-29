using OOPT4Project.Extension;
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

		public double Compare(Gene gene)
		{
			double variance = 0;
			foreach (GeneType type in UseGenes)
			{
				variance += Math.Abs(gene.GetGenom(type) - this.GetGenom(type));
			}
			return variance;
		}

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
				return Math.Clamp(val + (rng.NextDouble() * 2 - 1) * SimulationModel.MutationRange, 0, 1);
			else
				return val;
		}

		private static double RandomGenomValue()
		{
			Random rnd = SimulationModel.Generator;
			return rnd.NextDouble();
		}

		public double GetGenom(GeneType geneType) => Genom[(int)geneType];

		public double MapGenom(double genom, double from, double to) => genom.Map(0, 1, from, to);

		public double GetMapGenom(GeneType geneType, double from, double to)
		{
			double val = Genom[(int)geneType];
			return MapGenom(val, from, to);
		}

		private static readonly double StepAdjFactor = 10;
		public CreatureStats GetStats()
		{
			double size = GetMapGenom(GeneType.Size, 0.25, 1);
			double metabs = GetMapGenom(GeneType.MetabolismSpeed, 0.35, 1);
			double aware = GetMapGenom(GeneType.Awareness, 0.35, 1);
			double reprate = GetMapGenom(GeneType.ReproduceRate, 0.3, 0.5);
			double carniv = GetMapGenom(GeneType.Carnivorousness, 0, 1);

			return new CreatureStats()
			{
				EnergyResource = size / 5,
				HealthMax = (size * 2 + metabs * 1),
				HealingRate = metabs / 5,
				Carnivorousness = carniv > 0.5 ? 1 : 0,
				HungerRate = (metabs * 3 + size * 2 + aware) / 6 / StepAdjFactor,
				ThirstRate = (size * 2 + aware) / 6 / StepAdjFactor,
				Stealth = (metabs),
				Strength = (size * 1 + carniv * 3) / 4 / StepAdjFactor,
				Awareness = aware,
				ReproduceRate = (reprate * 2.1 + metabs * 1.9) / 4 / 8.5,
				Age = ((1 - metabs) * 0.9 + size) / 1.9 * 140,
			};
		}
	}
}
