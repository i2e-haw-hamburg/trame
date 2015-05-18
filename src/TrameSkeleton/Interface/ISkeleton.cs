using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Trame
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K">4 vector</typeparam>
    /// <typeparam name="T"> 3 vector</typeparam>
    public interface ISkeleton<K, T> : IEquatable<ISkeleton<K, T>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="j"></param>
        void UpdateSkeleton(JointType jt, IJoint<K, T> j);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        IJoint<K, T> GetJoint(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        IJoint<K, T> Root { get; set; }
        /// <summary>
        /// 
        /// </summary>
        UInt32 Timestamp { get; }
        /// <summary>
        /// 
        /// </summary>
        UInt32 ID { get; }
        /// <summary>
        /// 
        /// </summary>
        bool Valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ISkeleton<K, T> GetArms();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ISkeleton<K, T> Clone();
        /// <summary>
        /// 
        /// </summary>
        IJoint<K, T> GetHead();
    }
}
