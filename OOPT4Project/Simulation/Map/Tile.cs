using OOPT4Project.Simulation.Creature;
using System;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class Tile : ISimulated
	{
		public Coordinates Coordinates { get; private set; }
		public TileClimate TileClimate { get; private set; }
		public List<CreatureEntity> CreatureList { get; private set; } = new();
		public TileType Type { get; private set; }
		public bool CanWalkTo { get; private set; }

		private List<CreatureEntity> _toRegister = new();
		private List<CreatureEntity> _toUnregister = new();

		private double _foodResource;
		private double _waterResource;

		private TileTypeResources _resources;

		public Tile(Coordinates coor, TileType type)
		{
			TileClimate = new TileClimate();

			Coordinates = coor;
			Type = type;

			TileTypeLogic.Resources.TryGetValue(type, out TileTypeResources resources);
			TileTypeLogic.CanWalkTo.TryGetValue(type, out bool walk);
			CanWalkTo = walk;

			_resources = resources;
			_foodResource = resources.InitialFood;
			_waterResource = resources.InitialWater;
		}

		public void RegisterCreature(CreatureEntity creature)
		{
			_toRegister.Add(creature);
		}

		public void UnregisterCreature(CreatureEntity creature)
		{
			_toUnregister.Add(creature);
		}

		public void SimulateStep()
		{
			foreach (CreatureEntity creature in CreatureList)
				creature.SimulateStep();		
		}

		public void EndStep()
		{
			if(_toRegister.Count > 0)
			{
				CreatureList.AddRange(_toRegister);
				_toRegister.Clear();
			}
			if(_toUnregister.Count > 0)
			{
				_toUnregister.ForEach(x => CreatureList.Remove(x));
				_toUnregister.Clear();
			}
			
		}
	}
}