using System;
using System.Runtime.InteropServices;
using System.Threading;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Math;

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
		{}


        public void Stop()
        {
            running = false;
            t?.Join();
        }

	    public void Start()
	    {
            t = new Thread(Run);
            t.Start();
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
