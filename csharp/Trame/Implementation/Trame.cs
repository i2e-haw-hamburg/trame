using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame.Implementation.Skeleton;

namespace Trame
{
    public class Trame : ICameraAbstraction
    {
        Thread t = null;
        ISkeleton last = null;

        public Trame()
        {
            last = Creator.InvalidSkeleton;

            t = new Thread(this.run);
            t.Start();
        }

        ISkeleton ICameraAbstraction.GetSkeleton()
        {
            // return copy of last element
            return last;
        }

        public event Action<ISkeleton> NewSkeleton;


        private void run()
        {
            while (true)
            {
                FireNewSkeleton(Creator.DefaultSkeleton);
                Thread.Sleep(1000);
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
