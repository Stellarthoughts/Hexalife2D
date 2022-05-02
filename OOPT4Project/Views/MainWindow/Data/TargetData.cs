using OOPT4Project.Simulation.Creature;

namespace OOPT4Project.Views.MainWindow.Data
{
	public class TargetData
	{
		public string? Name { get; set; }
		public CreatureType Type { get; set; }
		public int Age { get; set; }
		public bool Hungry { get; set; }
		public bool Thirsty { get; set; }
		public bool Needy { get; set; }
	}
}
