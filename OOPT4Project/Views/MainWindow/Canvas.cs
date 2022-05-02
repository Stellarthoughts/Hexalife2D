using OOPT4Project.Render;
using OOPT4Project.Simulation.Map;
using OOPT4Project.Views.MainWindow.Data;
using SkiaSharp;
using SkiaSharp.Views.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		// Canvas
		private readonly CanvasCamera _camera;
		private readonly SKElement _view;

		// Drawer
		private readonly SimulationDrawer _simulationDrawer;
		private readonly float _tileSize = 15;

		// Selection
		private Tile _selectedTile = null!;

		private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
		{
			SKCanvas canvas = e.Surface.Canvas;
			canvas.Clear();

			float width = (float)_view.ActualWidth;
			float height = (float)_view.ActualHeight;

			_camera.SetGlobalOffset(width / 2, height / 2);

			canvas.DrawRect(new SKRect(0, 0, width, height), BackgroundPaint);

			_simulationDrawer.Draw(canvas, _camera);

			canvas.DrawCircle(width / 2, height / 2, 3, CrosshairPaint);

			ClimateDrawer.DrawClimate(canvas, _simulationModel.MapController.MapClimate.ClimateType, width, height);
			BorderDrawer.DrawHexagonalBorder(canvas, BorderPaint, new SKPoint(0, 0), new SKPoint(0, height), 40);
			BorderDrawer.DrawHexagonalBorder(canvas, BorderPaint, new SKPoint(width, 0), new SKPoint(width, height), 40);
		}

		private void SkElement1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var mainWindow = Application.Current.MainWindow;
			var dpiScale = VisualTreeHelper.GetDpi(mainWindow);

			var dpiScaleX = dpiScale.DpiScaleX;
			var dpiScaleY = dpiScale.DpiScaleY;

			var pixelPosition = e.GetPosition(sender as IInputElement);
			var scaledPixelPosition = new System.Windows.Point(pixelPosition.X * dpiScaleX, pixelPosition.Y * dpiScaleY);

			var tile = _simulationDrawer.GetTileFromPixel(scaledPixelPosition);
			if (tile != null)
			{
				if (SelectedTile == tile)
				{
					SelectedTile = null!;
					TileType = 0;
					TileFood = 0;
					TileWater = 0;
					TargetData = new();
				}
				else
				{
					SelectedTile = tile;
				}
				FetchDataFromTile();

				_simulationDrawer.SelectTile(SelectedTile);
				_view.InvalidateVisual();
			}
		}

		private void FetchDataFromTile()
		{
			if (SelectedTile == null)
				return;

			var newData = new List<TargetData>();
			SelectedTile.CreatureList.ForEach(x => newData.Add(new()
			{
				Name = x.UniqueName,
				Type = x.Type,
				Age = x.GetCurrentAge(),
				Hungry = x.HungerSatisfied(),
				Thirsty = x.ThirstSatisfied(),
				Needy = x.ReproduceSatisfied()
			}));
			TargetData = newData;
			TileType = SelectedTile.Type;
			TileFood = SelectedTile.GetFoodCount();
			TileWater = SelectedTile.GetWaterCount();
		}
	}
}
