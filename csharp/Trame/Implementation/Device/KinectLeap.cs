﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AForge.Math;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
    class KinectLeap : IDevice
    {
        readonly KinectDevice kinect = new KinectDevice();
        readonly LeapMotion leap = new LeapMotion();
        readonly LeapAdapter adapter = new LeapAdapter();
        private Thread t;
        private bool running = true;

        public KinectLeap()
        {
            t = new Thread(Run);
            t.Start();
        }
        
        private void Run()
        {
            while (running)
            {
                FireNewSkeleton();
            }
        }

        public ISkeleton<Vector4, Vector3> GetSkeleton()
        {
            var s = kinect.GetSkeleton();
            return GetSkeleton(s);
        }

        public ISkeleton<Vector4, Vector3> GetSkeleton(ISkeleton<Vector4, Vector3> s)
        {
            return leap.GetSkeleton(s);
        }

        public void Stop()
        {
            kinect.Stop();
            leap.Stop();
            running = false;
            t.Join();
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
