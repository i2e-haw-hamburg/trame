using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Trame.Implementation.Skeleton;

namespace Trame
{
    public class Trame : ITrame
    {
        Thread t = null;
        ISkeleton last = null;

        public Trame()
        {
            last = new Skeleton();

            t = new Thread(this.run);
            t.Start();
        }

        ISkeleton ITrame.GetSkeleton()
        {
            // return copy of last element
            return last;
        }

        public event Action<ISkeleton> NewSkeleton;


        private void run()
        {
            while (true)
            {
                FireNewSkeleton(new Skeleton());
                Thread.Sleep(1000);
            }
        }

        private void FireNewSkeleton(Skeleton skeleton)
        {
            last = skeleton;

            if (NewSkeleton != null)
            {
                NewSkeleton(skeleton);
            }
        }
    
    }
}
