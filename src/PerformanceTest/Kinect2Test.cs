using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Trame;

namespace PerformanceTest
{
    internal class Kinect2Test
    {
        private Trame.Trame _trame;
        private IList<ISkeleton> _skeletons;

        public Kinect2Test()
        {
            _trame = new Trame.Trame(DeviceType.KINECT2);
        }

        public void Run()
        {
            _skeletons = new List<ISkeleton>();
            Console.WriteLine("Starting Kinect v2 sensor..");
            _trame.Start();
            Console.WriteLine("Kinect sensor running. Press Enter to quit test.");
            _trame.NewSkeleton += TrameOnNewSkeleton;

            using (var timer = new System.Threading.Timer(Callback, null, 0, 500))
            {
                Console.ReadKey();
            }
        }

        private void TrameOnNewSkeleton(ISkeleton skeleton)
        {
            Console.WriteLine("Found new skeleton with id {0}.", skeleton.ID);
            _skeletons.Add(skeleton);
        }

        private void Callback(object state)
        {
            try
            {
                var skeleton = _trame.GetSkeleton();
                if (skeleton != null && skeleton.ID != 0)
                {
                    Console.WriteLine("Tracking skeleton({0})'s head at [{1}, {2}, {3}].",
                    skeleton.ID,
                    skeleton.GetHead().Point.X,
                    skeleton.GetHead().Point.Y,
                    skeleton.GetHead().Point.Z
                    );
                }
                else
                {
                    Console.WriteLine("No skeleton tracked.");
                }

                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
        }
    }
}