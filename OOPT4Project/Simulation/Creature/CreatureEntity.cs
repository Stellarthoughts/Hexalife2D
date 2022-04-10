using OOPT4Project.Simulation.Creature.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
    public class CreatureEntity
    {
        public Gene Gene { get; private set; }
        public Stats Stats { get; private set; }

        public IBehavior CurrentBehavior { get; private set; }

        public CreatureEntity(Gene gene)
        {
            this.Gene = gene;
            Stats = gene.CreateStats();
            CurrentBehavior = new SearchBehavior();
        }


    }
}
