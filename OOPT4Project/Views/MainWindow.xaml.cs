using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using OOPT4Project.Render;
using OOPT4Project.Simulation;
using System;
using System.Windows;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace OOPT4Project.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		SimulationModel _simulationModel;
		SimulationDrawer _simulationDrawer;

		public MainWindow()
		{
			InitializeComponent();
			_simulationModel = new SimulationModel();
			_simulationDrawer = new SimulationDrawer(_simulationModel);
			//_simulationModel.SimulateStep();
		}

		private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
		{
			ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };
			float width = (float) SkElement1.ActualWidth;
			float height = (float) SkElement1.ActualHeight;

			canvas.FillColor = Color.FromArgb("44AEB5");
			canvas.FillRectangle(0, 0, width, height);

			_simulationDrawer.Draw(canvas, width, height);

			canvas.DrawCircle(width / 2, height / 2, 3);
		}
	}
}
