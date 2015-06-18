using System;
using System.Threading;
using Trame.Implementation.Device.Adapter;

namespace Trame.Implementation.Device
{
	/// <summary>
	/// Kinect leap.
	/// </summary>
    class KinectLeap : IDevice
    {
        readonly KinectDevice kinect = new KinectDevice();
        readonly LeapMotion leap = new LeapMotion();
        readonly LeapAdapter adapter = new LeapAdapter();
        private Thread t;
        private bool running = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Device.KinectLeap"/> class.
		/// </summary>
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

        public ISkeleton GetSkeleton()
        {
            var s = kinect.GetSkeleton();
            return GetSkeleton(s);
        }

        public ISkeleton GetSkeleton(ISkeleton s)
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

        public event Action<ISkeleton> NewSkeleton;
    }
}
