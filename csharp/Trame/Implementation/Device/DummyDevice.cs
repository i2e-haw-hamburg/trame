using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using AForge.Math;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
    class DummyDevice : IDevice
    {
        private Thread t;
        private bool running = true;

        public DummyDevice()
        {
            t = new Thread(Run);
            t.Start();
        }


        public void Stop()
        {
            running = false;
            t.Join();
        }

        private void Run()
        {
            while (running)
            {
                Thread.Sleep(20);
                FireNewSkeleton();
            }
        }

        public ISkeleton<Vector4, Vector3> GetSkeleton()
        {
            return Creator.GetNewDefaultSkeleton();
        }

        public ISkeleton<Vector4, Vector3> GetSkeleton(ISkeleton<Vector4, Vector3> baseSkeleton)
        {
            return baseSkeleton;
        }


        private void FireNewSkeleton()
        {
            if (NewSkeleton != null)
            {
                NewSkeleton(GetSkeleton());
            }
        }

        public event Action<ISkeleton<Vector4, Vector3>> NewSkeleton;
    }
}
