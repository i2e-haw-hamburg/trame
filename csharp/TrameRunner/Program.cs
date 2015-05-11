using System;
using System.Threading;
using Trame;
using TrameSerialization;

namespace TrameRunner
{
    class Program
    {
        Thread t = new Thread(Run);
        readonly ICameraAbstraction trame = new Trame.Trame(DeviceType.KINECT);
        readonly static Timer timer = new Timer();
        static long countOfSkeletons = 0;
        private static bool run = true;

        static void Run()
        {
            timer.Start();
            var skeleton = Trame.Implementation.Skeleton.Creator.GetNewDefaultSkeleton();
            var serializer = new Serialization(OutputType.PROTOBUF);

            while (run)
            {
                var foo = serializer.Serialize(skeleton);
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Current time: {0} - skeletons {1}", timer.Now(), countOfSkeletons++);
                
            }
        }

        static void Main()
        {
            var p = new Program();
            p.t.Start();

            Console.WriteLine("Press key to stop program\n");
            Console.ReadKey();
            run = false;
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
