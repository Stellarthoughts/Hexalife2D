using OOPT4Project.Render;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using SkiaSharp.Views.WPF;
using System;
using System.Windows.Threading;

namespace OOPT4Project.Views.Main
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		private readonly SimulationModel _simulationModel;
		private int _seedValue;

		private readonly SimulationDrawer _simulationDrawer;
		private readonly CanvasCamera _camera;
		private readonly SKElement _view;
		private Tile _selectedTile = null!;
		private readonly float _tileSize = 15;

		private readonly DispatcherTimer _simulationTimer = new();
		private double _timerInterval = 300;
		private readonly double _timerIncrement = 2;

		private Tile SelectedTile
		{
			get
			{
				return _selectedTile;
			}
			set
			{
				_selectedTile = value;
			}
		}

		public MainWindow()
		{
			InitializeComponent();
			UpdateLayout();

			_simulationModel = new SimulationModel();
			_simulationModel.CreateMapRandom(200, TileTypeLogic.ProbWeightsDefault, 0.1);
			_simulationModel.PopulateSimulation(400);

			_simulationDrawer = new SimulationDrawer(_simulationModel, _tileSize);
			_camera = new(new CameraSettings(500, 500, 1, 6));

			_view = SkElement1;
			_view.IgnorePixelScaling = true;

			_simulationTimer.Interval = System.TimeSpan.FromMilliseconds(_timerInterval);
			_simulationTimer.Tick += SimulationTimer_Tick;
		}

		private void SimulationTimer_Tick(object? sender, EventArgs e)
		{
			_simulationModel.SimulateStep();
			_view.InvalidateVisual();
		}

		private void UpdateTimer()
		{
			_simulationTimer.Interval = TimeSpan.FromMilliseconds(_timerInterval);
		}

	}
}
