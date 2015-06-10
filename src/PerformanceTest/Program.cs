using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame;
using Trame.Implementation.Skeleton;

namespace PerformanceTest
{
    class Program
    {
        readonly ICameraAbstraction _trame = new Trame.Trame(DeviceType.EMPTY);
        readonly Timer _t = new Timer();

        long _countOfSkeletons = 1000;


        void Run()
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
            //p._trame.NewSkeleton += p.Update;
            p.Run();

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
        }

        private void Update(ISkeleton obj)
        {
            _countOfSkeletons++;
        }

        private void CloneSkeletons()
        {
            
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
