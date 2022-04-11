using System;
using System.Collections.Generic;

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

        public void SimulateStep()
        {
            throw new NotImplementedException();
        }
    }
}
