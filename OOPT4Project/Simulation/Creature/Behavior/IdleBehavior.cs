using OOPT4Project.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class IdleBehavior : AbstractBehavior
	{
		private CreatureEntity _creatureEntity;

		public IdleBehavior(CreatureEntity creatureEntity)
		{
			_creatureEntity = creatureEntity;
		}

		public override bool DoBehavior()
		{
			MoveRandom(_creatureEntity);
			return true;
		}
	}
}
