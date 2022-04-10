using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation
{
    public static class SimulationParams
    {
        // Simulation params
        public static int RandomSeed { get; set; } = 1;
        public static double ChanceToBeMale { get; set; } = 0.5;

        // Simaltion service entities

        public static Random Generator { get; set; } = new Random(RandomSeed);

        // Genome defaults
        public static double MetabolismSpeed { get; set; } = 1;
    }
}
