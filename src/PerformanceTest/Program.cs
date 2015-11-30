using System;
using Trame;
using Trame.Implementation.Skeleton;

namespace PerformanceTest
{
    class Program
    {
        private ICameraAbstraction _trame;
        readonly Timer _t = new Timer();

        long _countOfSkeletons = 0;


        void CloneSkeletonTest()
        {
            _t.Start();
            var skeleton = Creator.GetNewDefaultSkeleton<Skeleton>();
            uint id = 0;
            for (int i = 0; i < _countOfSkeletons; i++)
            {
                skeleton = skeleton.Clone();
                var arms = skeleton.GetArms();
                id = skeleton.ID;
            }
            Console.WriteLine("Current time: {0}ms - skeletons cloned {1}", _t.Now(), _countOfSkeletons);

            _t.Start();
            skeleton = Creator.GetNewDefaultSkeleton<InMapSkeleton>();
            for (int i = 0; i < _countOfSkeletons; i++)
            {
                skeleton = skeleton.Clone();
                var arms = skeleton.GetArms();
                id = skeleton.ID;
                
            }
            Console.WriteLine("Current time: {0}ms - in map skeletons cloned {1}", _t.Now(), _countOfSkeletons);
            Console.WriteLine(id);
        }

        static void Main(string[] args)
        {
            //var p = new Program();
            //p.LeapTest();

            new Kinect2Test().Run();
        }

        private void Stop()
        {
            _trame.Stop();
        }

        private void LeapTest()
        {
            _trame = new Trame.Trame(DeviceType.EMPTY);
            _trame.SetDevice(DeviceType.KINECT);
            _trame.Start();
            _t.Start();
            _trame.NewSkeleton += skeleton =>
            {
                ++_countOfSkeletons;
                Console.WriteLine(_countOfSkeletons);
                Console.CursorTop -= 1;
            };

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
            Console.WriteLine("{0}ms - skeletons created {1} - {2} fps", _t.Now(), _countOfSkeletons, 1000 * _countOfSkeletons / _t.Now());
            Stop();
            Console.ReadKey();
        }

    }

    class Timer
    {
        DateTime start;
        DateTime stop;

        public void Start()
        {
            start = DateTime.Now;
        }

        public void Holt()
        {
            stop = DateTime.Now;
        }

        public void Reset()
        {
            start = stop;
        }

        public long Milli()
        {
            return (long)(stop - start).TotalMilliseconds;
        }

        public long Now()
        {
            return (long)(DateTime.Now - start).TotalMilliseconds;
        }
    }
}
