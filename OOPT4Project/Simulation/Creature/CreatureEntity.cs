using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
    public class CreatureEntity
    {
        public Gene gene { get; private set; }

        public CreatureEntity(Gene gene)
        {
            this.gene = gene;
        }


    }
}
