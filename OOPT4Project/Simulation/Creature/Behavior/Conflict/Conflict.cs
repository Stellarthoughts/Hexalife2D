using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class Conflict
	{
		private readonly CreatureEntity _hunter;
		private readonly CreatureEntity _prey;

		public Conflict(CreatureEntity hunter, CreatureEntity prey)
		{
			_hunter = hunter;
			_prey = prey;
		}

		public static double CheckStat(double stat) => stat - SimulationModel.Generator.NextDouble();

		public bool Resolve()
		{
			CreatureStats hSt = _hunter.Stats; 
			CreatureStats pSt = _prey.Stats;
			
			if(CheckStat(hSt.Stealth) > CheckStat(pSt.Awareness))
			{
				bool pw = false;
				bool hw = false;

				do
				{
					hw = _prey.DealDamage(hSt.Strength);
					pw = _hunter.DealDamage(pSt.Strength);
				}
				while(!(hw || pw));

				if (hw)
					return true;
				else
					return false;
			}
			else
			{
				return false;
			}
		}
	}
}
