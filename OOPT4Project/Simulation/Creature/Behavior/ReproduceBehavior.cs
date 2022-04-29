using OOPT4Project.Simulation.Map;
using System.Linq;

namespace OOPT4Project.Simulation.Creature.Behavior
{
	public class ReproduceBehavior : AbstractBehavior
	{
		private readonly CreatureEntity _creature;

		public ReproduceBehavior(CreatureEntity creatureEntity)
		{
			_creature = creatureEntity;
		}

		public override bool DoBehavior()
		{
			Tile tile = _creature.CurrentTile;
			var tileMates = tile.CreatureList.FindAll(x => x.Gene.IsMale != _creature.Gene.IsMale);

			if (tileMates.Count > 0)
			{
				CreatureEntity mate = tileMates.MinBy(x => x.Gene.Compare(_creature.Gene))!;
				if (!mate.Gene.IsMale)
					mate.GiveBirth(_creature);
				else
					_creature.GiveBirth(mate);

				_creature.SatisfyReproduce(_creature.ReproduceValue());
				mate.SatisfyReproduce(mate.ReproduceValue());
			}
			else
			{
				MoveToNeighboorMaxBy(_creature, x => x.CreatureList.Count);
			}

			if (_creature.ReproduceSatisfied())
				return true;
			else
				return false;
		}
	}
}
