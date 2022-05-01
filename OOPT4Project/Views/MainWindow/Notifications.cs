using OOPT4Project.Simulation.Creature;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private void CreatureDeathNotification(object? sender, CreatureEventArgs e)
		{
			_deathCounter++;
		}

		private void CreatureBornNotification(object? sender, CreatureEventArgs e)
		{
			_bornCounter++;
		}
	}
}
