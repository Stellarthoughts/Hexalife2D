using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private void SkElement1_MouseDown(object sender, MouseButtonEventArgs e)
		{
			var mainWindow = Application.Current.MainWindow;
			var dpiScale = VisualTreeHelper.GetDpi(mainWindow);

			var dpiScaleX = dpiScale.DpiScaleX;
			var dpiScaleY = dpiScale.DpiScaleY;

			var pixelPosition = e.GetPosition(sender as IInputElement);
			var scaledPixelPosition = new System.Windows.Point(pixelPosition.X * dpiScaleX, pixelPosition.Y * dpiScaleY);

			var tile = _simulationDrawer.GetTileFromPixel(scaledPixelPosition);
			if(tile != null)
			{
				SelectedTile = tile;
				_simulationDrawer.SelectTile(tile);
				_view.InvalidateVisual();
			}
		}
	}
}
