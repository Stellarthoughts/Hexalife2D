using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Map
{
    public class Map
    {
        public List<Tile> Tile { get; private set; } = new List<Tile>();

        public Map()
        {
            Regenerate();
        }
        public void Regenerate()
        {

        }
    }
}
