using Microsoft.Maui.Graphics;

namespace OOPT4Project.Render
{
	public interface IDrawer
	{
		void Draw(ICanvas canvas, CanvasCamera camera);
	}
}
