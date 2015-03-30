using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;
using Trame;

namespace Trame
{
    public interface ICameraAbstraction
    {
        /// <summary>
        /// The method uses the configured device and don't serialize the fetched result.
        /// </summary>
        /// <returns>the current skeleton</returns>
        ISkeleton<Vector4, Vector3> GetSkeleton();

        /// <summary>
        /// Event is fired if a new skeleton was created.
        /// </summary>
        event Action<ISkeleton<Vector4, Vector3>> NewSkeleton;

        void SetDevice(DeviceType t);

        void Stop();
    }
}
