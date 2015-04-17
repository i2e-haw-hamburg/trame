using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AForge.Math;
using Trame;

namespace TrameRunner
{
    class Program
    {
        readonly ICameraAbstraction trame = new Trame.Trame(DeviceType.KINECT);
        readonly Timer t = new Timer();

        long countOfValidSkeletons = 0;
        long countOfSkeletons = 0;


        void Run()
        {
            t.Start();
            while (true)
            {
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Current time: {0} - skeletons {1}", t.Now(), countOfSkeletons);
                Thread.Sleep(17);
            }
        }

        static void Main(string[] args)
        {
            var p = new Program();
            p.trame.NewSkeleton += p.Update;
            p.Run();

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
        }

        private void Update(ISkeleton<Vector4, Vector3> obj)
        {
            if (obj.Valid)
            {
                countOfValidSkeletons++;
            }
            countOfSkeletons++;
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
