using AForge.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            IJoint parent = new OrientedJoint();
            foreach (var child in list)
            {
                parent.AddChild(child);
            }
            return parent;
        }

        public static IJoint CreateHead()
        {
            return Default.CreateHead();
        }
    }
}
