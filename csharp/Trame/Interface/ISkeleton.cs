using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trame
{
    public interface ISkeleton : IEquatable<ISkeleton>
    {
        bool UpdateSkeleton(JointType jt, IJoint j);
        IJoint GetJoint(JointType jt);
        IJoint GetRoot();
        UInt32 GetTimestamp();
        UInt32 GetID();
        bool IsValid();
    }
}
