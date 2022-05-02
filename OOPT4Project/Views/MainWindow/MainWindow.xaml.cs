using OOPT4Project.Render;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace OOPT4Project.Views.Main
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window, INotifyPropertyChanged
	{
		// Model
		private readonly SimulationModel _simulationModel;

		// Timer
		private readonly DispatcherTimer _simulationTimer = new();
		private double _timerInterval = 300;
		private readonly double _timerIncrement = 2;

		// Property changed
		public event PropertyChangedEventHandler? PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null!)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private Tile SelectedTile
		{
			get => _selectedTile;
			set => _selectedTile = value;
		}

		public MainWindow()
		{
			// Defaults
			AssignDefaultChances();

			// Window init
			InitializeComponent();
			UpdateLayout();

			// Model init
			_simulationModel = new SimulationModel();
			_simulationModel.CreatureBorn += CreatureBornNotification;
			_simulationModel.CreatureDeath += CreatureDeathNotification;
			_simulationModel.CreateMapRandom(MapSize, TileTypeLogic.ProbWeightsDefault, SuddenSwitch);
			_simulationModel.PopulateSimulation(StartPopulation);
			_simulationModel.Init();

			// Drawing
			_simulationDrawer = new SimulationDrawer(_simulationModel, _tileSize);
			_camera = new(new CameraSettings(500, 500, 1, 6));

			// View
			_view = SKElement;
			_view.IgnorePixelScaling = true;

			// Timer
			_simulationTimer.Interval = System.TimeSpan.FromMilliseconds(_timerInterval);
			_simulationTimer.Tick += SimulationTimer_Tick;
		}

		private void SimulationTimer_Tick(object? sender, EventArgs e)
		{
			_simulationModel.SimulateStep();
			FetchDataFromTile();
			_view.InvalidateVisual();
		}

		private void UpdateTimer()
		{
			_simulationTimer.Interval = TimeSpan.FromMilliseconds(_timerInterval);
		}
	}
}
