using System.Collections.Generic;

namespace Trame.Implementation.Skeleton
{
    public class Creator
    {
        public static ISkeleton GetNewDefaultSkeleton()
        {
            return Default.CreateSkeleton();
        }

        public static float GetDefaultBoneLength(JointType jt)
        {
            return Default.Lengths[jt];
        }

        public static ISkeleton GetNewInvalidSkeleton()
        {
            var s = new Skeleton();
            s.Valid = false;
            return s;
        }

        public static IJoint CreateParent(IEnumerable<IJoint> list)
        {
            var parent = new OrientedJoint();
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
