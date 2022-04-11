using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Map
{
    public class Tile : ISimulated
    {
        public Coordinates Coordinates { get; private set; }
        public TileClimate TileClimate { get; private set; }

        public TileType Type { get; private set; }

        private double foorResource;
        private double waterResource;

        public Tile(Coordinates coor, TileType type)
        {
            Coordinates = coor;
            Type = type;
        }
    }
}