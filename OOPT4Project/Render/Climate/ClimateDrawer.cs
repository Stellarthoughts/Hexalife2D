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
			Color = SKColors.White,
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

			SKPaint rectPaint = new()
			{
				Style = SKPaintStyle.Fill,
				Color = SKColor.Parse("0C91A4"),
			};

			SKPoint offset = new(width / 2, 30);

			canvas.DrawRect(0, 0, width, height, paint);
			if (drawName)
			{
				ClimateTypeLogic.ClimateTypeToNames.TryGetValue(type, out var name);
				var blob = SKTextBlob.Create(name, new SKFont(SKTypeface.FromFamilyName("Inria Serif"), 24), offset);
				canvas.DrawRoundRect(new SKRoundRect(
					new SKRect(blob.Bounds.Left - blob.Bounds.Width / 2 - 5,
								blob.Bounds.Top - 30,
								blob.Bounds.Right - blob.Bounds.Width / 2 + 3 + 5,
								blob.Bounds.Bottom + 10
								), 10),
					rectPaint);

				canvas.DrawText(blob, -blob.Bounds.Width / 2 + 7, 0, CenteredTextPaint);
			}
		}
	}
}
