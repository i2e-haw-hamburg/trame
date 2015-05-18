using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrameSkeleton.Math
{
    public static class Convert
    {
        private static Matrix TransformationMatrix
        {
            get
            {
                var m = new Matrix();
                m.M11 = 1;
                m.M22 = 1;
                m.M33 = 1;
                m.M44 = 1;
                m.M14 = 700;
                m.M24 = -300;
                m.M34 = 180;
                return m;
            }
        }

        public static Vector3 InternalToWorldCoordinate(Vector3 vec)
        {
            var correctCoordinates = new Vector4(vec.X, vec.Z, vec.Y, 1);
            return new Vector3(correctCoordinates.X, correctCoordinates.Y, correctCoordinates.Z);
        }

        public static Vector3 TransformToWorldSpace(Vector3 vector3)
        {
            return vector3;
        }
    }
}