﻿using System;
using System.Threading;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
	/// <summary>
	/// Dummy device.
	/// </summary>
    class DummyDevice : IDevice
    {
        private Thread t;
        private bool running = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Device.DummyDevice"/> class.
		/// </summary>
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

        public ISkeleton GetSkeleton()
        {
            return Creator.GetNewDefaultSkeleton();
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
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

        public event Action<ISkeleton> NewSkeleton;
    }
}