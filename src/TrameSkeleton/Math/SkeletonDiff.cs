using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trame;
using TrameSkeleton.Math;
using Trame.Implementation.Skeleton;

namespace TrameSkeleton.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SkeletonDiff
    {
        private IJoint<Vector4, Vector3> root;

        /// <summary>
        /// 
        /// </summary>
        public IJoint<Vector4, Vector3> Root
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
        public static IJoint<Vector4, Vector3> Diff(IJoint<Vector4, Vector3> j1, IJoint<Vector4, Vector3> j2)
        {
            if (j1.JointType != j2.JointType)
            {
                throw new Exception("Joint types defer from each other.");
            }
            var newJoint = new OrientedJoint<Vector4, Vector3>(j1.JointType, j1.Valid)
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

        public static IJoint<Vector4, Vector3> Div(IJoint<Vector4, Vector3> j, int divisor)
        {
            var newJoint = new OrientedJoint<Vector4, Vector3>(j.JointType, j.Valid);
            newJoint.Point = j.Point / divisor;
            newJoint.Orientation = j.Orientation / divisor;
            foreach (var child in j.GetChildren())
            {
                newJoint.AddChild(Div(child, divisor));
            }

            return newJoint; 
        }

        public static IJoint<Vector4, Vector3> Add(IJoint<Vector4, Vector3> j1, IJoint<Vector4, Vector3> j2)
        {
            var newJoint = new OrientedJoint<Vector4, Vector3>(j1.JointType, j1.Valid);
            newJoint.Point = j1.Point + j2.Point;
            newJoint.Orientation = j1.Orientation + j2.Orientation;
            foreach (var child in j1.GetChildren())
            {
                newJoint.AddChild(Add(child, j2.FindChild(child.JointType)));
            }

            return newJoint;
        }
    }
}
