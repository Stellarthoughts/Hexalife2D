using Microsoft.Maui.Graphics;
using System;
using System.Linq;

namespace OOPT4Project.Render.Camera
{
	public struct CameraSettings
	{
		public double HorizontalBorder;
		public double VerticalBorder;
		public double MinScale;
		public double MaxScale;

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
		private double _centerX = 0;
		private double _centerY = 0;

		private double _targetX = 0;
		private double _targetY = 0;

		private double _offsetX = 0;
		private double _offsetY = 0;

		private double _scale = 1;
		private double _scaleTarget = 1;

		private double _horizontalBorder;
		private double _verticalBorder;

		private double _minScale;
		private double _maxScale;

		public CanvasCamera(CameraSettings set)
		{
			_horizontalBorder = set.HorizontalBorder;
			_verticalBorder = set.VerticalBorder;
			_minScale = set.MinScale;
			_maxScale = set.MaxScale;
		}

		public void Update()
		{
			_centerX = _targetX;
			_centerY = _targetY;
			_scale = _scaleTarget;
		}

		public void SetGlobalOffset(double x, double y)
		{
			_offsetX = x;
			_offsetY = y;
		}

		public void SetTargetPosition(double x, double y)
		{
			_targetX = Math.Clamp(x,-_horizontalBorder,_horizontalBorder);
			_targetY = Math.Clamp(y,-_horizontalBorder,_horizontalBorder);
		}

		public void SetTargetScale(double z)
		{
			_scaleTarget = Math.Clamp(z, _minScale, _maxScale);
		}

		public void OffsetTargetPosition(double x, double y)
		{
			SetTargetPosition(_targetX + x, _targetY + y);
		}

		public void OffsetTargetScale(double z)
		{
			SetTargetScale(_scaleTarget + z);
		}

		public void Adjust(ref PathF adj)
		{
			Point avg = GetAverageCoordinate(adj);
			adj.Move(-(float)avg.X,-(float)avg.Y);
			adj = adj.AsScaledPath((float)_scale);
			adj.Move((float) (avg.X * _scale), (float) (avg.Y * _scale));
			adj.Move((float) (_offsetX - _centerX * _scale), (float) (_offsetY - _centerY * _scale));
		}

		public void Adjust(ref Point tilePoint)
		{
			double x = tilePoint.X;
			double y = tilePoint.Y;
			tilePoint = new Point(x * _scale,y * _scale);
			tilePoint = tilePoint.Offset(_offsetX - _centerX * _scale, _offsetY - _centerY * _scale);
		}

		public void Adjust(ref Size size)
		{
			size = new Size(size.Width * _scale, size.Height * _scale);
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
