using OOPT4Project.Simulation.Creature;
using System.Collections.Generic;
using System.Linq;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private readonly Queue<string> _notifications = new();
		public List<string> Notifications
		{
			get => _notifications.ToList();
		}

		private static readonly int MaxNotifications = 20;

		// Inspector
		private int _birthCounter = 0;
		private int _deathCounter = 0;

		public int BirthCounter
		{
			get => _birthCounter;
			set
			{
				_birthCounter = value;
				OnPropertyChanged("BirthCounter");
			}
		}

		public int DeathCounter
		{
			get => _deathCounter;
			set
			{
				_deathCounter = value;
				OnPropertyChanged("DeathCounter");
			}
		}

		private void CreatureDeathNotification(object? sender, CreatureEventArgs e)
		{
			DeathCounter++;
			var creature = e.Creature;
			CreatureStatusLogic.StatusToString.TryGetValue(creature.Status, out var status);
			AddNotification(
				creature.Type.ToString() + " "
				+ creature.UniqueName + " "
				+ status + ".");
		}

		private void CreatureBornNotification(object? sender, CreatureEventArgs e)
		{
			BirthCounter++;
			var creature = e.Creature;
			AddNotification(
				creature.Type.ToString() + " "
				+ creature.UniqueName + " "
				+ "was born!");
		}

		private void AddNotification(string str)
		{
			if (Notifications.Count >= MaxNotifications)
			{
				_notifications.Dequeue();
			}
			_notifications.Enqueue(str);

			OnPropertyChanged("Notifications");
		}
	}
}
