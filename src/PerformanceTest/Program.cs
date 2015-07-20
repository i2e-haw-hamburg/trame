using System;
using Trame;
using Trame.Implementation.Skeleton;

namespace PerformanceTest
{
    class Program
    {
        private ICameraAbstraction _trame = new Trame.Trame(DeviceType.EMPTY);
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
            var p = new Program();
            p.LeapTest();

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
            Console.WriteLine("{0}ms - skeletons created {1} - {2} fps", p._t.Now(), p._countOfSkeletons, 1000 * p._countOfSkeletons / p._t.Now());
            p.Stop();
            Console.ReadKey();
        }

        private void Stop()
        {
            _trame.Stop();
        }

        private void LeapTest()
        {
            _trame.SetDevice(DeviceType.LEAP_MOTION_AND_KINECT);

            _t.Start();
            _trame.NewSkeleton += skeleton => ++_countOfSkeletons;
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
