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

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame"/> class.
		/// </summary>
        public Trame() : this(DeviceType.EMPTY)
        {}
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame"/> class.
		/// </summary>
		/// <param name="dt">Dt.</param>
        public Trame(DeviceType dt)
        {
            last = Creator.GetNewInvalidSkeleton();
            currentType = dt;
            updatedType();
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
                currentDevice.Stop();
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
        {}
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
            Console.WriteLine("Close all resources");
            currentDevice.Stop();
        }
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Trame"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Trame"/>.</returns>
        public override string ToString()
        {
            return "Trame - Device: " + currentType.ToString();
        }
    }
}
