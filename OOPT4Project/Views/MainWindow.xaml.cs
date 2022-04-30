using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using OOPT4Project.Render;
using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using SkiaSharp.Views.WPF;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace OOPT4Project.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		private readonly SimulationModel _simulationModel;
		private readonly SimulationDrawer _simulationDrawer;
		private readonly CanvasCamera _camera;
		private readonly SKElement _view;

		private readonly DispatcherTimer _simulationTimer = new();

		private readonly double _tileSize = 15;

		private double _timerInterval = 300;
		private readonly double _timerIncrement = 2;
		private int _seedValue;

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

		private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
		{
			SkiaCanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
			float width = (float)_view.ActualWidth;
			float height = (float)_view.ActualHeight;

			_camera.SetGlobalOffset(width / 2, height / 2);

			canvas.FillColor = Color.FromArgb("44AEB5");
			canvas.FillRectangle(0, 0, width, height);

			_simulationDrawer.Draw(canvas, _camera);

			canvas.DrawCircle(width / 2, height / 2, 3);
			BorderDrawer.DrawHexagonalBorder(canvas, Color.FromArgb("0C91A4"), new Point(0, 0), new Point(0, height), 40);
			BorderDrawer.DrawHexagonalBorder(canvas, Color.FromArgb("0C91A4"), new Point(width, 0), new Point(width, height), 40);
		}

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			double delta = e.Delta / 1000.0;
			_camera.OffsetTargetScale(delta);
			_camera.Update();
			_view.InvalidateVisual();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			double x = 0;
			double y = 0;

			if (Keyboard.IsKeyDown(Key.A))
				x = -1;
			else if (Keyboard.IsKeyDown(Key.D))
				x = 1;
			if (Keyboard.IsKeyDown(Key.W))
				y = -1;
			else if (Keyboard.IsKeyDown(Key.S))
				y = 1;

			_camera.OffsetTargetPosition(x * 5, y * 5);
			_camera.Update();
			_view.InvalidateVisual();
		}

		private void SpeedUpButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_timerInterval /= _timerIncrement;
			UpdateTimer();
		}

		private void PlayButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			if (_simulationTimer.IsEnabled)
				_simulationTimer.Stop();
			else
				_simulationTimer.Start();
		}

		private void SpeedDownButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_timerInterval *= _timerIncrement;
			UpdateTimer();
		}

		private void StepButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_simulationModel.SimulateStep();
			_view.InvalidateVisual();
		}

		private void SeedTB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			try
			{
				_seedValue = Convert.ToInt32((sender as TextBox)!.Text);
			}
			catch(FormatException)
			{
				return;
			}
			catch(OverflowException)
			{
				return;
			}
		}

		private void PopSimButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_simulationModel.PopulateSimulation(50);
		}

		private void NewMapButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SimulationModel.RandomSeed = _seedValue;
			_simulationModel.CreateMapRandom(200, TileTypeLogic.ProbWeightsDefault, 0.1);
			_simulationModel.PopulateSimulation(400);
			_simulationDrawer.Init();
			_view.InvalidateVisual();
		}



	}
}
