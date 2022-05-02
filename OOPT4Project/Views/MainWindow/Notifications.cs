using OOPT4Project.Simulation.Creature;
using System.Collections.Generic;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private Queue<string> _notifications = new();
		public Queue<string> Notifications
		{
			get => _notifications; 
			set
			{
				_notifications = value;
				OnPropertyChanged("Notifications");
			}
		}

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
