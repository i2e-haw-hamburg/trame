using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
