using SkiaSharp;
using System;
using System.Linq;

namespace OOPT4Project.Render
{
	public struct CameraSettings
	{
		public float HorizontalBorder { get; private set; }
		public float VerticalBorder { get; private set; }
		public float MinScale { get; private set; }
		public float MaxScale { get; private set; }

		public CameraSettings(float horizontalBorder, float verticalBorder, float minScale, float maxScale)
		{
			HorizontalBorder = horizontalBorder;
			VerticalBorder = verticalBorder;
			MinScale = minScale;
			MaxScale = maxScale;
		}
	}
	public class CanvasCamera
	{
		private float _positionX = 0;
		private float _positionY = 0;

		private float _targetPositionX = 0;
		private float _targetPositionY = 0;

		private float _globalOffsetX = 0;
		private float _globalOffsetY = 0;

		private float _scale = 1;
		private float _scaleTarget = 1;

		private readonly float _horizontalBorder;
		private readonly float _verticalBorder;

		private readonly float _minScale;
		private readonly float _maxScale;

		public CanvasCamera(CameraSettings set)
		{
			_horizontalBorder = set.HorizontalBorder;
			_verticalBorder = set.VerticalBorder;
			_minScale = set.MinScale;
			_maxScale = set.MaxScale;
		}

		public void Update()
		{
			_positionX = _targetPositionX;
			_positionY = _targetPositionY;
			_scale = _scaleTarget;
		}

		public void SetGlobalOffset(float x, float y)
		{
			_globalOffsetX = x;
			_globalOffsetY = y;
		}

		public void OffsetGlobalOffset(float x, float y)
		{
			_globalOffsetX += x;
			_globalOffsetY += y;
		}

		public void SetTargetPosition(float x, float y)
		{
			_targetPositionX = Math.Clamp(x, -_horizontalBorder, _horizontalBorder);
			_targetPositionY = Math.Clamp(y, -_verticalBorder, _verticalBorder);
		}

		public void SetTargetScale(float z)
		{
			_scaleTarget = Math.Clamp(z, _minScale, _maxScale);
		}

		public void OffsetTargetPosition(float x, float y)
		{
			SetTargetPosition(_targetPositionX + x, _targetPositionY + y);
		}

		public void OffsetTargetScale(float z)
		{
			SetTargetScale(_scaleTarget + z);
		}

		public void Adjust(ref SKPath adj)
		{
			SKPoint avg = GetAverageCoordinate(adj);
			adj.Offset(-avg.X, -avg.Y);
			adj.Transform(SKMatrix.CreateScale(_scale, _scale));
			adj.Offset(avg.X * _scale, avg.Y * _scale);
			adj.Offset(_globalOffsetX - _positionX * _scale, _globalOffsetY - _positionY * _scale);
		}

		public void Adjust(ref SKPoint tilePoint)
		{
			float x = tilePoint.X;
			float y = tilePoint.Y;
			tilePoint = new SKPoint(x * _scale, y * _scale);
			tilePoint.Offset(_globalOffsetX - _positionX * _scale, _globalOffsetY - _positionY * _scale);
		}

		public void Adjust(ref SKSize size)
		{
			size = new SKSize(size.Width * _scale, size.Height * _scale);
		}

		internal void Adjust(ref SKBitmap bitmap)
		{
			var scaled = new SKBitmap((int)(bitmap.Width * _scale), (int)(bitmap.Height * _scale));
			bitmap.ScalePixels(scaled,SKFilterQuality.High);
			bitmap = scaled;
		}


		public void Adjust(ref double size)
		{
			size *= _scale;
		}

		public static SKPoint GetAverageCoordinate(SKPath path)
		{
			float avgX = 0;
			float avgY = 0;

			path.Points.Select(x => x.X).ToList().ForEach(x => avgX += x);
			path.Points.Select(x => x.Y).ToList().ForEach(x => avgY += x);

			avgX /= path.Points.Count();
			avgY /= path.Points.Count();

			return new SKPoint(avgX, avgY);
		}
	}
}
