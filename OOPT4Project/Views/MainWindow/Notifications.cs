using OOPT4Project.Simulation.Creature;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		// Inspector
		private int _bornCounter = 0;
		private int _deathCounter = 0;

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
