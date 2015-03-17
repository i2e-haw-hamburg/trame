using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
    class KinectLeap : IDevice
    {
        readonly KinectDevice kinect = new KinectDevice();
        readonly LeapMotion leap = new LeapMotion();

        public ISkeleton GetSkeleton()
        {
            ISkeleton s = kinect.GetSkeleton();
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
        }
    }
}
