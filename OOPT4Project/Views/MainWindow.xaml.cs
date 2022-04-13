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

			/*canvas.StrokeColor = Colors.White.WithAlpha(.5f);
			canvas.StrokeSize = 2;
			for (int i = 0; i < 100; i++)
			{
				float x = Random.Shared.Next((int)SkElement1.ActualWidth);
				float y = Random.Shared.Next((int)SkElement1.ActualHeight);
				float r = Random.Shared.Next(5, 50);
				canvas.DrawCircle(x, y, r);
			}*/
		}
	}
}
