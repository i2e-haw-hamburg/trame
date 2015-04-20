using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;

namespace TrameSkeleton.Math
{
    public static class Convert
    {
        public static Vector3 InternalToWorldCoordinate(Vector3 vec)
        {
            return new Vector3(-vec.X, -vec.Z, vec.Y);
        }
    }
}
