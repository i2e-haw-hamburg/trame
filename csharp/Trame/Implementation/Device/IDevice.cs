using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;

namespace Trame.Implementation.Device
{
    interface IDevice
    {
        ISkeleton<Vector4, Vector3> GetSkeleton();
        ISkeleton<Vector4, Vector3> GetSkeleton(ISkeleton<Vector4, Vector3> baseSkeleton);
        void Stop();
        event Action<ISkeleton<Vector4, Vector3>> NewSkeleton;
    }
}
