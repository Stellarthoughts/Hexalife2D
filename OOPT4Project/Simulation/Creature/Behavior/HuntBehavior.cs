using OOPT4Project.Extension;
using OOPT4Project.Simulation.Map;
using System;
using System.Linq;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class HuntBehavior : AbstractBehavior
	{
		private readonly CreatureEntity _creature;
		public HuntBehavior(CreatureEntity creatureEntity)
		{
			_creature = creatureEntity;
		}

		public override bool DoBehavior()
		{
			double hunger = _creature.HungerValue();
			Tile tile = _creature.CurrentTile;

			if (tile.CreatureList.Count > 0)
			{
				CreatureEntity prey = tile.CreatureList.PickRandom(SimulationModel.Generator);
				Conflict conflict = new(_creature, prey);
				bool hunterWon = conflict.Resolve();
				if (hunterWon)
					_creature.SatisfyHunger(prey.Stats.EnergyResource);
				else
					prey.SatisfyHunger(_creature.Stats.EnergyResource);
			}
			else
			{
				MoveToNeighboorMaxBy(_creature, x => x.CreatureList.Count);
			}

			if (_creature.HungerSatisfied())
				return true;
			else
				return false;
		}
	}
}
