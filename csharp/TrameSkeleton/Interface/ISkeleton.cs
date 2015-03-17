using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Trame
{
    public interface ISkeleton : IEquatable<ISkeleton>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="j"></param>
        void UpdateSkeleton(JointType jt, IJoint j);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        IJoint GetJoint(JointType jt);
        /// <summary>
        /// 
        /// </summary>
        IJoint Root { get; set; }
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

        ISkeleton GetArms();
    }
}
