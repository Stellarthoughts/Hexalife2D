using SkiaSharp;

namespace OOPT4Project.Render
{
	public interface IDrawer
	{
		void Draw(SKCanvas canvas, CanvasCamera camera);
	}
}
