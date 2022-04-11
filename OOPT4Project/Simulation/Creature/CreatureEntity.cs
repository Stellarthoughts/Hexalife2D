using OOPT4Project.Simulation.Creature.Behavior;
using OOPT4Project.Simulation.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
    public class CreatureEntity : ISimulated
    {
        public Gene Gene { get; private set; }
        public Stats Stats { get; private set; }

        public IBehavior CurrentBehavior { get; private set; }

        public Tile CurrentTile { get; set; }

        public CreatureEntity(Gene gene, Tile tile)
        {
            Gene = gene;
            CurrentTile = tile;
            Stats = gene.CreateStats();
            CurrentBehavior = new SearchBehavior(this);
        }

        public void SimulateStep()
        {
            throw new NotImplementedException();
        }
    }
}
