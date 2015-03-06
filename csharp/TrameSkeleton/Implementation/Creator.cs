using AForge.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trame.Implementation.Skeleton
{
    public class Creator
    {
        public static ISkeleton GetNewDefaultSkeleton()
        {
            return Default.CreateSkeleton();
        }

        public static ISkeleton GetNewInvalidSkeleton()
        {
            var s = new Skeleton();
            s.Valid = false;
            return s;
        }

        public static IJoint CreateParent(IEnumerable<IJoint> list)
        {
            IJoint parent = new Joint();
            foreach (var child in list)
            {
                parent.AddChild(child);
            }
            return parent;
        }
    }
}
