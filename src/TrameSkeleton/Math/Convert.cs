
namespace TrameSkeleton.Math
{
    /// <summary>
    /// 
    /// </summary>
    public static class Convert
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector3 InternalToWorldCoordinate(Vector3 vec)
        {
            return new Vector3(vec.X, vec.Y, -vec.Z) / 1000;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector4 InternalToWorldCoordinate(Vector4 vec)
        {
            return new Vector4(vec.X, vec.Y, -vec.Z, -vec.W);
        }
    }
}