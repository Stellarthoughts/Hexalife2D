using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
	public struct CreatureStats
	{
		public double HealthMax { get; set; }
		public double HungerRate { get; set; }
		public double ThirstRate { get; set; }
		public double Strength { get; set; }
		public double ReproduceRate { get; set; }
		public double Carnivorousness { get; set; }
		public double Stealth { get; set; }
		public double Awareness { get; set; }
		public double EnergyResource { get; set; }
		public double HealingRate { get; set; }
		public double Age { get; set; }
	}
}
