﻿using OOPT4Project.Simulation.Creature;
using System.Collections.Generic;

namespace OOPT4Project.Simulation.Map
{
	public class Tile : ISimulated
	{
		public Coordinate Coordinates { get; private set; }
		public TileClimate TileClimate { get; private set; }
		public List<CreatureEntity> CreatureList { get; private set; } = new();
		public TileType Type { get; private set; }
		public bool CanWalkTo { get; private set; }

		private readonly List<CreatureEntity> _toRegister = new();
		private readonly List<CreatureEntity> _toUnregister = new();

		private double _foodResource;
		private double _waterResource;

		private TileTypeResources _resources;

		public Tile(Coordinate coor, TileType type)
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
			TileClimate.SimulateStep();

			_foodResource += _resources.ReplenishRateFood * TileClimate.FoodFactor;
			_waterResource += _resources.ReplenishRateWater * TileClimate.WaterFactor;

			foreach (CreatureEntity creature in CreatureList)
			{
				creature.SimulateStep();
				if (TileClimate.RandomBirth && SimulationModel.Generator.NextDouble() < TileClimate.BirthFactor)
					creature.GiveStrangeBirth();
			}
		}
		public double GetFoodCount() => _foodResource;
		public double GetWaterCount() => _waterResource;
		public double EatAmount(double amount)
		{
			if (amount >= _foodResource)
			{
				double actual = _foodResource;
				_foodResource = 0;
				return actual;
			}
			else
			{
				_foodResource -= amount;
				return amount;
			}
		}
		public double DrinkAmount(double amount)
		{
			if (amount >= _waterResource)
			{
				double actual = _waterResource;
				_waterResource = 0;
				return actual;
			}
			else
			{
				_waterResource -= amount;
				return amount;
			}
		}
		public void EndStep()
		{
			if (_toRegister.Count > 0)
			{
				CreatureList.AddRange(_toRegister);
				_toRegister.Clear();
			}
			if (_toUnregister.Count > 0)
			{
				_toUnregister.ForEach(x => CreatureList.Remove(x));
				_toUnregister.Clear();
			}

		}
	}
}