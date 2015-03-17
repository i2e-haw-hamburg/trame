using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
    class DummyDevice : IDevice
    {
        public ISkeleton GetSkeleton()
        {
            return Creator.GetNewInvalidSkeleton();
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            return baseSkeleton;
        }

        public void Stop()
        {
            
        }
    }
}
