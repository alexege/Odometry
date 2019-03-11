using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Odometry_Pose_Estimator
{
    class Program
    {
        static void Main(string[] args)
        {
            Tricycle robot = new Tricycle(0.1250, 0.1250, 0.964, 0.3673, 35136);
            Console.WriteLine(robot.estimate(1, 0, 35136, 0));

            Tricycle robot2 = new Tricycle(0.2, 0.2, 0.75, 1, 512);
            Console.WriteLine( robot2.estimate(1, 0, 512, 0));

            Tricycle robot3 = new Tricycle(0.2, 0.2, 0.75, 1, 512);
            Console.WriteLine(robot3.estimate(1, Math.PI/2, 512, 0));
        }
    }
}
