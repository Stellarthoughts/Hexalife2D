using OOPT4Project.Simulation;
using OOPT4Project.Simulation.Map;
using System;
using System.Windows.Controls;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private void SeedTB_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			try
			{
				_seedValue = Convert.ToInt32((sender as TextBox)!.Text);
			}
			catch (FormatException)
			{
				return;
			}
			catch (OverflowException)
			{
				return;
			}
		}

		private void PopSimButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_simulationModel.PopulateSimulation(200);
		}

		private void NewMapButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			SimulationModel.RandomSeed = _seedValue;
			_simulationModel.CreateMapRandom(200, TileTypeLogic.ProbWeightsDefault, 0.1);
			_simulationModel.PopulateSimulation(400);
			_simulationModel.Init();

			_simulationDrawer.Recalculate();

			_view.InvalidateVisual();
		}
	}
}
