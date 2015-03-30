using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;

namespace Trame
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K">4 vector</typeparam>
    /// <typeparam name="T">3 vector</typeparam>
    public interface IJoint<K, T> : IEquatable<IJoint<K, T>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IJoint<K,T>> GetChildren();
        /// <summary>
        /// 
        /// </summary>
        K Orientation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        T Point { get; set; }
        /// <summary>
        /// 
        /// </summary>
        JointType JointType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        bool Valid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        bool AddChild(IJoint<K, T> j);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        bool RemoveChild(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        IJoint<K, T> FindChild(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        IJoint<K, T> DeepFind(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="j"></param>
        void Update(JointType jt, IJoint<K, T> j);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        IJoint<K, T> Append(IJoint<K, T> j);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IJoint<K, T> Clone();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="joints"></param>
        void AddChildren(IEnumerable<IJoint<K, T>> joints);
    }
}
