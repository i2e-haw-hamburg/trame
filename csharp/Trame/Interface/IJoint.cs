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
        Vector3 Normal { get; set; }
        Vector3 Point { get; set; }
        JointType JointType { get; set; }
        bool Valid { get; set; }
        bool AddChild(IJoint j);
        bool RemoveChild(JointType jt);
        IJoint FindChild(JointType jt);
        IJoint DeepFind(JointType jt);
        void Update(JointType jt, IJoint j);
        IJoint Append(IJoint j);
    }
}
