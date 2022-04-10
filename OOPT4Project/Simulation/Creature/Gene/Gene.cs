using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
    public enum GenomValues
    {
        MetabolismSpeed,

    }

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
            bool male = SimulationParams.Generator.NextDouble() > SimulationParams.ChanceToBeMale;
            return new Gene(male);
        }

        public Stats CreateStats()
        {
            Stats stats = new Stats();
            return stats;
        }
    }
}
