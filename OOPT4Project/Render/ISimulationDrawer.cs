using SkiaSharp;

namespace OOPT4Project.Render
{
	public interface ISimulationDrawer
	{
		void Draw(SKCanvas canvas, CanvasCamera camera);
	}
}
