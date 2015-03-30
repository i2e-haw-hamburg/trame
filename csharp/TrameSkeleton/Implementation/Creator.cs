using AForge.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trame.Implementation.Skeleton
{
    public class Creator
    {
        public static ISkeleton<Vector4, Vector3> GetNewDefaultSkeleton()
        {
            return Default.CreateSkeleton();
        }

        public static ISkeleton<Vector4, Vector3> GetNewInvalidSkeleton()
        {
            var s = new Skeleton<Vector4, Vector3>();
            s.Valid = false;
            return s;
        }

        public static IJoint<K, T> CreateParent<K, T>(IEnumerable<IJoint<K, T>> list) where K : new() where T : new()
        {
            var parent = new OrientedJoint<K, T>();
            foreach (var child in list)
            {
                parent.AddChild(child);
            }
            return parent;
        }

        public static IJoint<Vector4, Vector3> CreateHead()
        {
            return Default.CreateHead();
        }
    }
}
