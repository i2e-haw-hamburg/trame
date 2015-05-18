using System;
using Trame;
using Trame.Implementation.Skeleton;

namespace TrameSkeleton.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SkeletonDiff
    {
        private IJoint root;

        /// <summary>
        /// 
        /// </summary>
        public IJoint Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="j1"></param>
        /// <param name="j2"></param>
        /// <returns></returns>
        public static IJoint Diff(IJoint j1, IJoint j2)
        {
            if (j1.JointType != j2.JointType)
            {
                throw new Exception("Joint types defer from each other.");
            }
            var newJoint = new OrientedJoint(j1.JointType, j1.Valid)
            {
                Point = j1.Point - j2.Point,
                Orientation = j1.Orientation - j2.Orientation
            };
            foreach (var child in j1.GetChildren())
            {
                newJoint.AddChild(Diff(child, j2.FindChild(child.JointType)));
            }

            return newJoint;
        }

        public static IJoint Div(IJoint j, int divisor)
        {
            var newJoint = new OrientedJoint(j.JointType, j.Valid)
            {
                Point = j.Point/divisor,
                Orientation = j.Orientation/divisor
            };
            foreach (var child in j.GetChildren())
            {
                newJoint.AddChild(Div(child, divisor));
            }

            return newJoint; 
        }

        public static IJoint Add(IJoint j1, IJoint j2)
        {
            var newJoint = new OrientedJoint(j1.JointType, j1.Valid)
            {
                Point = j1.Point + j2.Point,
                Orientation = j1.Orientation + j2.Orientation
            };
            foreach (var child in j1.GetChildren())
            {
                newJoint.AddChild(Add(child, j2.FindChild(child.JointType)));
            }

            return newJoint;
        }
    }
}
