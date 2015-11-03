extern alias KinectV2;

using System;

namespace Trame.Implementation.Device
{
    internal class KinectV2Device : IDevice
    {
        public KinectV2Device()
        {

        }

        public ISkeleton GetSkeleton()
        {
            throw new NotImplementedException();
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public event Action<ISkeleton> NewSkeleton;
    }
}
