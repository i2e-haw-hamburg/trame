using System;

namespace Trame.Implementation.Device
{
    interface IDevice
    {
        ISkeleton GetSkeleton();
        ISkeleton GetSkeleton(ISkeleton baseSkeleton);
        void Stop();
        event Action<ISkeleton> NewSkeleton;
    }
}
