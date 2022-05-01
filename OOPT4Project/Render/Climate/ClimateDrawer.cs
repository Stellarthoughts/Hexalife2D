using OOPT4Project.Simulation.Map;
using SkiaSharp;

namespace OOPT4Project.Render
{
	public class ClimateDrawer
	{
		public static readonly SKPaint CenteredTextPaint = new()
		{
			Style = SKPaintStyle.Fill,
			TextAlign = SKTextAlign.Center,
			Color = SKColors.Black,
		};

		public static void DrawClimate(SKCanvas canvas, ClimateType type, float width, float height, bool drawName = true)
		{
			ClimateVisual.ClimateTypeToColorMask.TryGetValue(type, out var color);
			color = color.WithAlpha(70);

			SKPaint paint = new()
			{
				Style = SKPaintStyle.Fill,
				Color = color,
				BlendMode = SKBlendMode.Plus,
			};

			canvas.DrawRect(0, 0, width, height, paint);
			if (drawName)
			{
				ClimateTypeLogic.ClimateTypeToNames.TryGetValue(type, out var name);
				var blob = SKTextBlob.Create(name, new SKFont(SKTypeface.Default, 16));
				canvas.DrawText(blob, width / 2 - blob.Bounds.Width / 2, 20, CenteredTextPaint);
			}
		}
	}
}
