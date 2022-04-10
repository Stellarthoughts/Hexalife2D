using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Map
{
    public struct CubeCoordinates
    {
        int q;
        int r;
        int s;  
    }

    public struct AxialCoordinates
    {
        int q;
        int r;
    }

    public class Coordinates
    {
        public CubeCoordinates coordinates;

        public Coordinates()
        {

        }

    }
}
