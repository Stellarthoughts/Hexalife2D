using Microsoft.Maui.Graphics;

namespace OOPT4Project.Render
{
	public class CreatureDrawer : IDrawer
	{
		private double _tileSize;

		public CreatureDrawer(System.Collections.Generic.List<Simulation.Map.Tile> _tiles, double tileSize)
		{
			_tileSize = tileSize;
		}

		public void Draw(ICanvas canvas, CanvasCamera camera)
		{
			//throw new System.NotImplementedException();
		}
	}

}
