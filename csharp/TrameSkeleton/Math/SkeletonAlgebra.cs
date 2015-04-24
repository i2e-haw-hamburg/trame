using AForge.Math;
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
            double x = q.X;
            double y = q.Y;
            double z = q.Z;
            double w = q.W;

            // convert rotation quaternion to Euler angles in degrees
            double yawD, pitchD, rollD;
            pitchD = System.Math.Atan2(2 * ((y * z) + (w * x)), (w * w) - (x * x) - (y * y) + (z * z)) / System.Math.PI * 180.0;
            yawD = System.Math.Asin(2 * ((w * y) - (x * z))) / System.Math.PI * 180.0;
            rollD = System.Math.Atan2(2 * ((x * y) + (w * z)), (w * w) + (x * x) - (y * y) - (z * z)) / System.Math.PI * 180.0;


            double increment = 0.5;
            var pitch = (int)(System.Math.Floor((pitchD + ((increment / 2.0) * (pitchD > 0 ? 1.0 : -1.0))) / increment) * increment);
            var yaw = (int)(System.Math.Floor((yawD + ((increment / 2.0) * (yawD > 0 ? 1.0 : -1.0))) / increment) * increment);
            var roll = (int)(System.Math.Floor((rollD + ((increment / 2.0) * (rollD > 0 ? 1.0 : -1.0))) / increment) * increment);

            return new Vector3((float)pitch, (float)yaw, (float)roll);
        }
    }
}
