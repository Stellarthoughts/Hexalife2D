using OOPT4Project.Render;
using SkiaSharp;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
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

		private void Window_MouseWheel(object sender, MouseWheelEventArgs e)
		{
			float delta = e.Delta / 1000f;
			_camera.OffsetTargetScale(delta);
			_camera.Update();
			_view.InvalidateVisual();
		}

		private void Window_KeyDown(object sender, KeyEventArgs e)
		{
			float x = 0;
			float y = 0;

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
	}
}
