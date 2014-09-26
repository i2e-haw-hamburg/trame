using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trame
{
    public interface ISkeleton : IEquatable<ISkeleton>
    {
        void UpdateSkeleton(JointType jt, IJoint j);
        IJoint GetJoint(JointType jt);
        IJoint Root { get; set; }
        UInt32 Timestamp { get; }
        UInt32 ID { get; }
        bool Valid { get; set; }
    }
}
