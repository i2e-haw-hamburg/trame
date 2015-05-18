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

        public Trame() : this(DeviceType.EMPTY)
        {}

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

        public override string ToString()
        {
            return "Trame - Device: " + currentType.ToString();
        }
    }
}
