using System;
using System.Threading;
using System.Timers;
using Trame.Implementation.Device;
using Trame.Implementation.Skeleton;
using Timer = System.Timers.Timer;

namespace Trame
{
    public class Trame : ICameraAbstraction
    {
        Thread t = null;
        ISkeleton last = null;
        private DeviceType currentType;
        private IDevice currentDevice = null;
        private DateTime lastUpdate;
        private Timer resetTimer;
        private bool resetInProgress = false;

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

            resetTimer = new Timer(1000);
            resetTimer.AutoReset = true;
            lastUpdate = DateTime.Now;

            resetTimer.Elapsed += (sender, args) =>
            {
                if ((dt == DeviceType.KINECT || dt == DeviceType.LEAP_MOTION_AND_KINECT) && !resetInProgress && (DateTime.Now - lastUpdate).TotalMilliseconds > 2000)
                {
                    resetInProgress = true;
                    // reset trame
                    UpdatedType();
                    currentDevice.Start();

                    resetInProgress = false;
                    lastUpdate = DateTime.Now;
                }
            };

            UpdatedType();
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
            UpdatedType();
        }

        private void UpdatedType()
        {
            resetTimer.Stop();
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
                case DeviceType.KINECT2:
                    currentDevice = new KinectV2Device();
                    break;
                case DeviceType.LEAP_MOTION:
                    currentDevice = new LeapMotion();
                    break;
                case DeviceType.LEAP_MOTION_AND_KINECT:
                    currentDevice = new KinectLeap();
                    break;
                default:
                    currentDevice = new DummyDevice();
                    break;
            }
            currentDevice.NewSkeleton += FireNewSkeleton;
            resetTimer.Start();
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
		    lastUpdate = DateTime.Now;

		    NewSkeleton?.Invoke(skeleton);
        }

        public void Stop()
        {
            Console.WriteLine("Close all resources");
            currentDevice.NewSkeleton -= FireNewSkeleton;
            currentDevice.Stop();
        }

        public void Start()
        {
            currentDevice.Start();
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
