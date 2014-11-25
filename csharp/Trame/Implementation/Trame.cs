using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame.Implementation.Device;
using Trame.Implementation.Skeleton;

namespace Trame
{
    public class Trame : ICameraAbstraction
    {
        Thread t = null;
        ISkeleton last = null;


        public Trame()
        {
            last = Creator.GetNewInvalidSkeleton();

            t = new Thread(this.Run);
            t.Start();
        }

        ISkeleton ICameraAbstraction.GetSkeleton()
        {
            // return copy of last element
            return last;
        }

        public event Action<ISkeleton> NewSkeleton;


        private void Run()
        {
            IDevice device = new LeapMotion();

            while (true)
            {
                FireNewSkeleton(device.GetSkeleton());
            }
        }

        private void FireNewSkeleton(ISkeleton skeleton)
        {
            last = skeleton;

            if (NewSkeleton != null)
            {
                NewSkeleton(skeleton);
            }
        }
    
    }
}
