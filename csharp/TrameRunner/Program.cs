using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame;

namespace TrameRunner
{
    class Program
    {
        readonly ICameraAbstraction trame = new Trame.Trame();
        readonly Timer t = new Timer();


        void Run(int sec, bool hold)
        {
            var list = new List<ISkeleton>();
            ISkeleton tmp = null;
            long countOfValidSkeletons = 0;
            long countOfSkeletons = 0;
            var last = 0;
            
            t.Start();
            while (sec * 1000 > t.Now())
            {
                var proc = Process.GetCurrentProcess();
                tmp = trame.GetSkeleton();
                if (hold)
                {
                    list.Add(tmp);
                }
                if (tmp.Valid)
                {
                    countOfValidSkeletons++;
                }
                countOfSkeletons++;
                var time = (int)(t.Now()/1000);
                if (time > last)
                {
                    Console.WriteLine(string.Join(";", new long[] { time, countOfSkeletons, countOfValidSkeletons, proc.PrivateMemorySize64 }));
                    last = time;
                }
            }
        }

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run(100, true);

            Console.WriteLine("Press key to stop program\n");
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
