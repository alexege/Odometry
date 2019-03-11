using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odometry_Pose_Estimator
{
    class Point
    {
        public double x;
        public double y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point movePointAtAngle(double angle, double distance)
        {

            return new Point(x + Math.Cos(angle) * distance, y + Math.Sin(angle) * distance);
        }

        public double computeDistanceTo(Point p)
        {
            double dx = p.x - x;
            double dy = p.y - y;

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double computeHeadingTo(Point p)
        {
            double dx = p.x - x;
            double dy = p.y - y;
            
            return Math.Atan2(dy, dx);
        }
    }
}
