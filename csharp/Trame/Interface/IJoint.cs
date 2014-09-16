using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;

namespace Trame
{
    public interface IJoint : IEquatable<IJoint>
    {
        IList<IJoint> GetChildren();
        Vector3 GetNormal();
        Vector3 GetPoint();
        JointType GetJointType();
        bool IsValid();
        bool AddChild(IJoint j);
        bool RemoveChild(JointType jt);
        IJoint FindChild(JointType jt);
        IJoint DeepFind(JointType jt);
        bool Update(JointType jt, IJoint j);
    }
}
