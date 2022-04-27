using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class SearchWaterBehavior : IBehavior
	{
		private CreatureEntity _creatureEntity;

		public SearchWaterBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public bool DoBehavior()
		{
			return true;
		}
	}
}
