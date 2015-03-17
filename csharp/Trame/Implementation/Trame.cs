using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame.Implementation.Device;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;

namespace Trame
{
    public class Trame : ICameraAbstraction
    {
        Thread t = null;
        ISkeleton last = null;
        private DeviceType currentType = DeviceType.EMPTY;
        private IDevice currentDevice = null;
        private bool _keepRunning = true;


        public Trame()
        {
            last = Creator.GetNewInvalidSkeleton();
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
            }
        }

        private void Run()
        {
            while (_keepRunning)
            {
                FireNewSkeleton(currentDevice.GetSkeleton());
            }
            // close all open ressources
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
    
    }
}
