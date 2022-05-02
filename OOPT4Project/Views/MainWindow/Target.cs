using OOPT4Project.Simulation.Map;
using OOPT4Project.Views.MainWindow.Data;
using System.Collections.Generic;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private List<TargetData> _targetData = new();
		private TileType _tileType;
		private double _tileWater;
		private double _tileFood;

		public List<TargetData> TargetData
		{
			get => _targetData; set
			{
				_targetData = value;
				OnPropertyChanged("TargetData");
			}
		}

		public TileType TileType { get => _tileType; set
			{
				_tileType = value;
				OnPropertyChanged("TileType");
			}
		}
		public double TileWater { get => _tileWater; set { 
				_tileWater = value;
				OnPropertyChanged("TileWater");
			} 
		}
		public double TileFood { get => _tileFood; set { 
				_tileFood = value;
				OnPropertyChanged("TileFood");
			} 
		}
	}
}
