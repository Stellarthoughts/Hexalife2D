using SkiaSharp;

namespace OOPT4Project.Views.Main
{
	public partial class MainWindow : System.Windows.Window
	{
		private static readonly SKPaint BackgroundPaint = new()
		{
			Style = SKPaintStyle.Fill,
			Color = SKColor.Parse("44AEB5")
		};

		private static readonly SKPaint CrosshairPaint = new()
		{
			Style = SKPaintStyle.Stroke,
			Color = SKColors.Black,
			StrokeWidth = 1f,
			IsAntialias = true,
		};

		private static readonly SKPaint BorderPaint = new()
		{
			Style = SKPaintStyle.Fill,
			Color = SKColor.Parse("0C91A4"),
		};
	}
}
