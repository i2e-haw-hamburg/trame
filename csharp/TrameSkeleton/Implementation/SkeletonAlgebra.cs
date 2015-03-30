using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AForge.Math;
using Trame;
using Trame.Implementation.Skeleton;

namespace TrameSkeleton.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public static class SkeletonAlgebra
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static SkeletonDiff Diff(ISkeleton<Vector4, Vector3> s1, ISkeleton<Vector4, Vector3> s2)
        {
            var skelDiff = new SkeletonDiff();
            skelDiff.Root = SkeletonDiff.Diff(s1.Root, s2.Root);
            return skelDiff;
        }

        public static ISkeleton<Vector4, Vector3> SkeletonSmoothing(ISkeleton<Vector4, Vector3> s1, ISkeleton<Vector4, Vector3> s2, int windowSize)
        {
            var mean = Div(Diff(s1, s2), windowSize);

            return Add(s1, mean);
        }

        public static ISkeleton<Vector4, Vector3> Add(ISkeleton<Vector4, Vector3> s, SkeletonDiff diff)
        {
            var newSkeleton = new Skeleton<Vector4, Vector3>();
            newSkeleton.Root = SkeletonDiff.Add(s.Root, diff.Root);
            return newSkeleton;
        }

        public static SkeletonDiff Div(SkeletonDiff skelDiff, int divisor)
        {
            var sD = new SkeletonDiff();
            sD.Root = SkeletonDiff.Div(skelDiff.Root, divisor);

            return sD;
        }
    }
}
