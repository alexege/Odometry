using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odometry_Pose_Estimator
{
    class Tricycle
    {
        public double front_wheel_radius_meters;
        public double back_wheels_radius_meters;
        public double distance_from_front_wheel_back_axis;
        public double distance_between_rear_wheels;
        public int ticks_per_rev;
        public Point rearPosition;
        public Point steeringPosition;

        public Tricycle(double front_wheel_radius_meters, double back_wheels_radius_meters, double distance_from_front_wheel_back_axis, double distance_between_rear_wheels, int ticks_per_rev)
        {
            this.front_wheel_radius_meters = front_wheel_radius_meters;
            this.back_wheels_radius_meters = back_wheels_radius_meters;
            this.distance_from_front_wheel_back_axis = distance_from_front_wheel_back_axis;
            this.distance_between_rear_wheels = distance_between_rear_wheels;
            this.ticks_per_rev = ticks_per_rev;

            rearPosition = new Point(0, 0);
            steeringPosition = new Point(distance_from_front_wheel_back_axis, 0);
        }


        public Tuple<double, double, double> estimate(double time, double steering_angle, int encoder_ticks, double angular_velocity)
        {
            //Compute distance to travel
            double distance = (2 * Math.PI * front_wheel_radius_meters) / ticks_per_rev * encoder_ticks;

            //Move front wheel as directed
            Point nextSteeringPosition = steeringPosition.movePointAtAngle(steering_angle, distance);

            //Compute distance to rear - length = distance to catchup
            double catchupDistance = rearPosition.computeDistanceTo(nextSteeringPosition) - distance_from_front_wheel_back_axis;

            //Calculate new heading. Start with old, go to new
            double headingToCatchup = rearPosition.computeHeadingTo(nextSteeringPosition);

            //Travel catchup distance into heading direction
            Point nextRearPosition = rearPosition.movePointAtAngle(headingToCatchup, catchupDistance);

            //set new tricycle location
            steeringPosition = nextSteeringPosition;
            rearPosition = nextRearPosition;
            double heading = rearPosition.computeHeadingTo(steeringPosition);
            
            var tuple = new Tuple<double, double, double>(rearPosition.x, rearPosition.y, heading);

            return tuple;
        }
    }
}
