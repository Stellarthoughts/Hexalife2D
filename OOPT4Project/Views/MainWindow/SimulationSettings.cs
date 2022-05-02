using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using System.Collections.Generic;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		// Settings
		private static readonly int DefaultStartPopulation = 400;
		private static readonly double DefaultSuddenSwitch = 0.1;
		private static readonly int DefaultMapSize = 200;

		private double _grassChance;
		private double _lakeChance;
		private double _desertChance;
		private double _badlandChance;
		private double _hillsChance;
		private double _marshChance;
		private double _suddenSwitch = DefaultSuddenSwitch;
		private int _startPopulation = DefaultStartPopulation;
		private int _mapSize = DefaultMapSize;
		public int Seed { get; set; } = 1;
		public int MapSize
		{
			get => _mapSize; set
			{
				_mapSize = value;
				OnPropertyChanged("MapSize");
			}
		}
		public double GrassChance
		{
			get => _grassChance; set
			{
				_grassChance = value;
				OnPropertyChanged("GrassChance");
			}
		}
		public double LakeChance
		{
			get => _lakeChance; set
			{
				_lakeChance = value;
				OnPropertyChanged("LakeChance");
			}
		}
		public double DesertChance
		{
			get => _desertChance; set
			{
				_desertChance = value;
				OnPropertyChanged("DesertChance");
			}
		}
		public double BadlandChance
		{
			get => _badlandChance; set
			{
				_badlandChance = value;
				OnPropertyChanged("BadlandChance");
			}
		}
		public double HillsChance
		{
			get => _hillsChance; set
			{
				_hillsChance = value;
				OnPropertyChanged("HillsChance");
			}
		}
		public double MarshChance
		{
			get => _marshChance; set
			{
				_marshChance = value;
				OnPropertyChanged("MarshChance");
			}
		}
		public double SuddenSwitch
		{
			get => _suddenSwitch; set
			{
				_suddenSwitch = value;
				OnPropertyChanged("SuddenSwitch");
			}
		}
		public int StartPopulation
		{
			get => _startPopulation; set
			{
				_startPopulation = value;
				OnPropertyChanged("StartPopulation");
			}
		}
		public int AddPopulation { get; set; } = 50;

		private void PopSimButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_simulationModel.PopulateSimulation(AddPopulation);
		}

		private void NewMapButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SimulationModel.RandomSeed = Seed;
			BirthCounter = 0;
			DeathCounter = 0;
			_notifications.Clear();
			OnPropertyChanged("Notifications");
			SelectedTile = null!;

			var chances = new Dictionary<TileType, double>()
			{
				{ TileType.Grass, GrassChance },
				{ TileType.Lake, LakeChance },
				{ TileType.Hills, HillsChance },
				{ TileType.Desert, DesertChance },
				{ TileType.Badland, BadlandChance },
				{ TileType.Marsh, MarshChance },
				{ TileType.Ocean, 0 }
			};
			_simulationModel.CreateMapRandom(MapSize, chances, SuddenSwitch);
			_simulationModel.PopulateSimulation(StartPopulation);
			_simulationModel.Init();
			_simulationDrawer.Recalculate();
			_view.InvalidateVisual();
		}
		private void ResetButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			AssignDefaultChances();
		}

		private void AssignDefaultChances()
		{
			TileTypeLogic.ProbWeightsDefault.TryGetValue(TileType.Grass, out double grass);
			TileTypeLogic.ProbWeightsDefault.TryGetValue(TileType.Lake, out double lake);
			TileTypeLogic.ProbWeightsDefault.TryGetValue(TileType.Desert, out double desert);
			TileTypeLogic.ProbWeightsDefault.TryGetValue(TileType.Badland, out double badland);
			TileTypeLogic.ProbWeightsDefault.TryGetValue(TileType.Hills, out double hills);
			TileTypeLogic.ProbWeightsDefault.TryGetValue(TileType.Marsh, out double marsh);

			GrassChance = grass;
			LakeChance = lake;
			DesertChance = desert;
			BadlandChance = badland;
			HillsChance = hills;
			MarshChance = marsh;
			StartPopulation = DefaultStartPopulation;
			SuddenSwitch = DefaultSuddenSwitch;
			MapSize = DefaultMapSize;
		}
	}
}
