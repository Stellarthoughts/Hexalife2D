using OOPT4Project.Simulation.Map;
using System.Collections.Generic;
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

			if (tile.CreatureList.Count > 1)
			{
				CreatureEntity? prey =
					tile.CreatureList
						.Except(new List<CreatureEntity>() { _creature })
						.Where(x => x.Gene.GetGenom(GeneType.Carnivorousness) < _creature.Gene.GetGenom(GeneType.Carnivorousness)
						&& x.Gene.GetGenom(GeneType.Carnivorousness) < 0.2)
						.MaxBy(x => x.Gene.GetGenom(GeneType.Size));

				if (prey != null)
				{
					Conflict conflict = new(_creature, prey);
					bool hunterWon = conflict.Resolve();
					if (hunterWon)
						_creature.SatisfyHunger(prey.Stats.EnergyResource);
				}
				else
				{
					MoveToNeighboorMaxBy(_creature, x => x.CreatureList.Count);
				}
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
