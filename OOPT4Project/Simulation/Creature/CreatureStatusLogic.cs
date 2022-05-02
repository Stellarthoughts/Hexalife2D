using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Simulation.Creature
{
	public static class CreatureStatusLogic
	{
		public static readonly Dictionary<CreatureStatus, string> StatusToString = new()
		{
			{ CreatureStatus.Alive, "is alive" },
			{ CreatureStatus.DeathHunted, "got hunted down" },
			{ CreatureStatus.DeathBotchedHunt, "died while hunting" },
			{ CreatureStatus.DeathThirst, "died of thirst" },
			{ CreatureStatus.DeathHunger, "died of hunger" },
			{ CreatureStatus.DeathReproduce, "died of loneliness" },
			{ CreatureStatus.Death, "died for some reason" },
		};
	}
}
