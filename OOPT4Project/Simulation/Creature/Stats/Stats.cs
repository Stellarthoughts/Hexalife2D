using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
    public class Stats
    {
        public double Hunger { get; private set; } = 100;
        public double Thirst { get; private set; } = 100;
        public double ReproduceNeed { get; private set; } = 0;
    }
}
