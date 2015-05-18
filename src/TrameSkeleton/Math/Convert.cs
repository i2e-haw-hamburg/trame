using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;

namespace TrameSkeleton.Math
{
    public static class Convert
    {
        private static Matrix4x4 TransformationMatrix{
            get
            {
                var m = new Matrix4x4();
                m.V00 = 1;
                m.V11 = 1;
                m.V22 = 1;
                m.V33 = 1;
                m.V03 = 700;
                m.V13 = -300;
                m.V23 = 180;
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
            var correctCoordinates = new Vector4(vector3.X, vector3.Y, vector3.Z, 1);
            var transformedVector = TransformationMatrix * correctCoordinates;
            return new Vector3(transformedVector.X, transformedVector.Y, transformedVector.Z);
        }
    }
}
