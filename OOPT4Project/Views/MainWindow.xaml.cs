using Microsoft.Maui.Graphics;
using Microsoft.Maui.Graphics.Skia;
using System;
using System.Windows;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace OOPT4Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SKElement_PaintSurface(object sender, SkiaSharp.Views.Desktop.SKPaintSurfaceEventArgs e)
        {
            ICanvas canvas = new SkiaCanvas() { Canvas = e.Surface.Canvas };

            canvas.FillColor = Colors.Navy;
            canvas.FillRectangle(0, 0, (float)SkElement1.ActualWidth, (float)SkElement1.ActualHeight);

            canvas.StrokeColor = Colors.White.WithAlpha(.5f);
            canvas.StrokeSize = 2;
            for (int i = 0; i < 100; i++)
            {
                float x = Random.Shared.Next((int)SkElement1.ActualWidth);
                float y = Random.Shared.Next((int)SkElement1.ActualHeight);
                float r = Random.Shared.Next(5, 50);
                canvas.DrawCircle(x, y, r);
            }
        }
    }
}
