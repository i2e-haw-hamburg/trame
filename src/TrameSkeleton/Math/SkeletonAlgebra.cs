using Trame;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Implementation;

namespace TrameSkeleton.Math
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
            var skelDiff = new SkeletonDiff {Root = SkeletonDiff.Diff(s1.Root, s2.Root)};
            return skelDiff;
        }

        public static ISkeleton<Vector4, Vector3> SkeletonSmoothing(ISkeleton<Vector4, Vector3> s1, ISkeleton<Vector4, Vector3> s2, int windowSize)
        {
            var mean = Div(Diff(s1, s2), windowSize);

            return Add(s1, mean);
        }

        public static ISkeleton<Vector4, Vector3> Add(ISkeleton<Vector4, Vector3> s, SkeletonDiff diff)
        {
            var newSkeleton = new Skeleton<Vector4, Vector3> {Root = SkeletonDiff.Add(s.Root, diff.Root)};
            return newSkeleton;
        }

        public static SkeletonDiff Div(SkeletonDiff skelDiff, int divisor)
        {
            var sD = new SkeletonDiff {Root = SkeletonDiff.Div(skelDiff.Root, divisor)};

            return sD;
        }

        /// <summary>
        /// Calculates the euler angles of the quaternion.
        /// </summary>
        /// <param name="q">The quaternion</param>
        /// <returns>Euler angles in a three dimensional vector</returns>
        public static Vector3 EulerAnglesFromQuarternion(Vector4 q)
        {
            var phi = System.Math.Atan2(2*(q.X*q.Y + q.Z*q.W), 1 - 2*(q.Y*q.Y + q.Z*q.Z));
            var rho = System.Math.Asin(2*(q.X*q.Z - q.W*q.Y));
            var tau = System.Math.Atan2(2 * (q.X * q.W + q.Y * q.Z), 1 - 2 * (q.Z * q.Z + q.W * q.W)); ;

            return new Vector3((float) phi, (float) rho, (float) tau);
        }
    }
}
