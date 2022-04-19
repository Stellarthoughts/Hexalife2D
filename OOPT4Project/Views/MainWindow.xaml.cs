using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using OOPT4Project.Render;
using OOPT4Project.Render.Camera;
using OOPT4Project.Simulation;
using SkiaSharp.Views.WPF;
using System;
using System.Windows.Input;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace OOPT4Project.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : System.Windows.Window
	{
		private SimulationModel _simulationModel;
		private SimulationDrawer _simulationDrawer;
		private CanvasCamera _camera;
		private SKElement _view;

		private double _tileSize = 15;

		public MainWindow()
		{
			InitializeComponent();
			UpdateLayout();
			_simulationModel = new SimulationModel();
			_simulationDrawer = new SimulationDrawer(_simulationModel, _tileSize);
			_camera = new();
			
			_view = SkElement1;
			//_simulationModel.SimulateStep();
		}

		private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
		{
			ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
			float width = (float)_view.ActualWidth;
			float height = (float)_view.ActualHeight;

			_camera.SetGlobalOffset(width / 2, height / 2);

			canvas.FillColor = Color.FromArgb("44AEB5");
			canvas.FillRectangle(0, 0, width, height);

			_simulationDrawer.Draw(canvas, _camera, new Point(0,0));

			canvas.DrawCircle(width / 2, height / 2, 3);
			BorderDrawer.DrawHexagonalBorder(canvas, Color.FromArgb("0C91A4"), new Point(0,0),     new Point(0,height), 40);
			BorderDrawer.DrawHexagonalBorder(canvas, Color.FromArgb("0C91A4"), new Point(width,0), new Point(width,height), 40);
		}

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			double delta = e.Delta / 520.0;
			_camera.OffsetTargetScale(delta);
			_camera.Update();
			_view.InvalidateVisual();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			double x = 0;
			double y = 0;

			if(e.Key == Key.A)
				x = -1;
			else if (e.Key == Key.D)
				x = 1;
			if (e.Key == Key.W)
				y = -1;
			else if (e.Key == Key.S)
				y = 1;	

			_camera.OffsetTargetPosition(x * 5, y * 5);
			_camera.Update();
			_view.InvalidateVisual();
		}
	}
}
