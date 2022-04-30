using Microsoft.Maui.Graphics;
using System;
using System.Linq;

namespace OOPT4Project.Render
{
	public struct CameraSettings
	{
		public double HorizontalBorder { get; private set; }
		public double VerticalBorder { get; private set; }
		public double MinScale { get; private set; }
		public double MaxScale { get; private set; }

		public CameraSettings(double horizontalBorder, double verticalBorder, double minScale, double maxScale)
		{
			HorizontalBorder = horizontalBorder;
			VerticalBorder = verticalBorder;
			MinScale = minScale;
			MaxScale = maxScale;
		}
	}
	public class CanvasCamera
	{
		private double _positionX = 0;
		private double _positionY = 0;

		private double _targetPositionX = 0;
		private double _targetPositionY = 0;

		private double _globalOffsetX = 0;
		private double _globalOffsetY = 0;

		private double _scale = 1;
		private double _scaleTarget = 1;

		private readonly double _horizontalBorder;
		private readonly double _verticalBorder;

		private readonly double _minScale;
		private readonly double _maxScale;

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

		public void SetGlobalOffset(double x, double y)
		{
			_globalOffsetX = x;
			_globalOffsetY = y;
		}

		public void OffsetGlobalOffset(double x, double y)
		{
			_globalOffsetX += x;
			_globalOffsetY += y;
		}

		public void SetTargetPosition(double x, double y)
		{
			_targetPositionX = Math.Clamp(x, -_horizontalBorder, _horizontalBorder);
			_targetPositionY = Math.Clamp(y, -_verticalBorder, _verticalBorder);
		}

		public void SetTargetScale(double z)
		{
			_scaleTarget = Math.Clamp(z, _minScale, _maxScale);
		}

		public void OffsetTargetPosition(double x, double y)
		{
			SetTargetPosition(_targetPositionX + x, _targetPositionY + y);
		}

		public void OffsetTargetScale(double z)
		{
			SetTargetScale(_scaleTarget + z);
		}

		public void Adjust(ref PathF adj)
		{
			Point avg = GetAverageCoordinate(adj);
			adj.Move(-(float)avg.X, -(float)avg.Y);
			adj = adj.AsScaledPath((float)_scale);
			adj.Move((float)(avg.X * _scale), (float)(avg.Y * _scale));
			adj.Move((float)(_globalOffsetX - _positionX * _scale), (float)(_globalOffsetY - _positionY * _scale));
		}

		public void Adjust(ref Point tilePoint)
		{
			double x = tilePoint.X;
			double y = tilePoint.Y;
			tilePoint = new Point(x * _scale, y * _scale);
			tilePoint = tilePoint.Offset(_globalOffsetX - _positionX * _scale, _globalOffsetY - _positionY * _scale);
		}

		public void Adjust(ref Size size)
		{
			size = new Size(size.Width * _scale, size.Height * _scale);
		}

		public void Adjust(ref double size)
		{
			size *= _scale;
		}

		public static Point GetAverageCoordinate(PathF path)
		{
			double avgX = 0;
			double avgY = 0;

			path.Points.Select(x => x.X).ToList().ForEach(x => avgX += x);
			path.Points.Select(x => x.Y).ToList().ForEach(x => avgY += x);

			avgX /= path.Points.Count();
			avgY /= path.Points.Count();

			return new Point(avgX, avgY);
		}
	}
}
