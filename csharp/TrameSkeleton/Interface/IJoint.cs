using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;

namespace Trame
{
    public interface IJoint : IEquatable<IJoint>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<IJoint> GetChildren();
        /// <summary>
        /// 
        /// </summary>
        Vector4 Orientation { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Vector3 Point { get; set; }
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
        bool AddChild(IJoint j);
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
        IJoint FindChild(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        IJoint DeepFind(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="j"></param>
        void Update(JointType jt, IJoint j);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        IJoint Append(IJoint j);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IJoint Clone();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="joints"></param>
        void AddChildren(IEnumerable<IJoint> joints);
    }
}
