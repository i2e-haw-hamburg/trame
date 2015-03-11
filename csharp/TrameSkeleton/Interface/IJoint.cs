﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;
using NetworkMessages.Trame;

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
        Vector3 Normal { get; set; }
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

        SkeletonMessage.Joint ToMessage();
    }
}
