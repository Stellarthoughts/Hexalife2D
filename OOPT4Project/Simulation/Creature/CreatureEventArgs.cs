using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
	public class CreatureEventArgs
	{
		public CreatureEntity Creature { get; private set; }

		public CreatureEventArgs(CreatureEntity creature)
		{
			Creature = creature;
		}
	}
}
