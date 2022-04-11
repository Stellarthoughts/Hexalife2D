using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Map
{
    public class MapController : ISimulated
    {
        public List<Tile> Tiles { get; private set; } = new List<Tile>();

        public MapController()
        {

        }
        public void CreateMap()
        {
        }
    }
}
