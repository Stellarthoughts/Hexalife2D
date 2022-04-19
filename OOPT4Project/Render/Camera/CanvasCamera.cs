using Microsoft.Maui.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPT4Project.Render.Camera
{

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

		public CanvasCamera()
		{

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
			_targetX = x;
			_targetY = y;
		}

		public void SetTargetScale(double z)
		{
			_scaleTarget = z;
			if (_scaleTarget < 0) _scaleTarget = 0;
		}

		public void OffsetTargetPosition(double x, double y)
		{
			_targetX += x;
			_targetY += y;
		}

		public void OffsetTargetScale(double z)
		{
			_scaleTarget += z;
			if (_scaleTarget < 0) _scaleTarget = 0;
		}

		public void Adjust(ref PathF adj)
		{
			Point avg = GetAverageCoordinate(adj);
			adj.Move(-(float)avg.X,-(float)avg.Y);
			adj = adj.AsScaledPath((float)_scale);
			adj.Move((float) (avg.X * _scale), (float) (avg.Y * _scale));
			adj.Move((float) (_offsetX - _centerX * _scale), (float) (_offsetY - _centerY * _scale));
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
