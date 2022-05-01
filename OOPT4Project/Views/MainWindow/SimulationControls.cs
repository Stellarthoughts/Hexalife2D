using System.Windows.Input;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
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
