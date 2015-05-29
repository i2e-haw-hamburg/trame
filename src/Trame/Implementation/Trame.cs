using System;
using System.Threading;
using Trame.Implementation.Device;
using Trame.Implementation.Skeleton;

namespace Trame
{
    public class Trame : ICameraAbstraction
    {
        Thread t = null;
        ISkeleton last = null;
        private DeviceType currentType;
        private IDevice currentDevice = null;
        private bool _keepRunning = true;

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Trame"/> class.
		/// </summary>
        public Trame() : this(DeviceType.EMPTY)
        {}
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Trame"/> class.
		/// </summary>
		/// <param name="dt">Dt.</param>
        public Trame(DeviceType dt)
        {
            last = Creator.GetNewInvalidSkeleton();
            currentType = dt;
            updatedType();

            t = new Thread(this.Run);
            t.Start();
        }

        public ISkeleton GetSkeleton()
        {
            // return copy of last element
            return last;
        }

        public event Action<ISkeleton> NewSkeleton;

        public void SetDevice(DeviceType t)
        {
            currentType = t;
            updatedType();
        }

        private void updatedType()
        {
            if (currentDevice != null)
            {
                currentDevice.NewSkeleton -= FireNewSkeleton;
            }
            switch (currentType)
            {
                case DeviceType.KINECT:
                    currentDevice = new KinectDevice();
                    break;
                case DeviceType.LEAP_MOTION:
                    currentDevice = new LeapMotion();
                    break;
                case DeviceType.LEAP_MOTION_AND_KINECT:
                    currentDevice = new KinectLeap();
                    break;
                case DeviceType.EMPTY:
                    currentDevice = new DummyDevice();
                    break;
                default:
                    currentDevice = new DummyDevice();
                    break;
            }
            currentDevice.NewSkeleton += FireNewSkeleton;
        }

		/// <summary>
		/// Run this instance.
		/// </summary>
        private void Run()
        {
            while (_keepRunning)
            {
                Thread.Sleep(100);
            }
            // close all open ressources
            Console.WriteLine("Close all resources");
            currentDevice.Stop();

        }
		/// <summary>
		/// Fires the new skeleton.
		/// </summary>
		/// <param name="skeleton">Skeleton.</param>
        private void FireNewSkeleton(ISkeleton skeleton)
        {
            last = skeleton;
           
            if (NewSkeleton != null)
            {
                NewSkeleton(skeleton);
            }
        }

        public void Stop()
        {
            this._keepRunning = false;
        }
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Trame.Trame"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Trame.Trame"/>.</returns>
        public override string ToString()
        {
            return "Trame - Device: " + currentType.ToString();
        }
    }
}
