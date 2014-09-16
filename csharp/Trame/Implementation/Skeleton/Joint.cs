using AForge.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trame.Implementation.Skeleton
{
    class Joint : IJoint
    {
        IDictionary<JointType, IJoint> children = new Dictionary<JointType, IJoint>();
        Vector3 normal;
        Vector3 point;
        bool isValid;
        JointType type;

        public IList<IJoint> GetChildren()
        {
            throw new NotImplementedException();
        }

        public Vector3 GetNormal()
        {
            return normal;
        }

        public Vector3 GetPoint()
        {
            return point;
        }

        public JointType GetJointType()
        {
            return type;
        }

        public bool IsValid()
        {
            return isValid;
        }

        public bool AddChild(IJoint j)
        {
            try
            {
                children.Add(j.GetJointType(), j);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool RemoveChild(JointType jt)
        {
            try
            {
                return children.Remove(jt);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IJoint FindChild(JointType jt)
        {
            try
            {
                if (children.ContainsKey(jt))
                {
                    return children[jt];
                }
                else
                {
                    throw new Exception("joint type not found");
                }
            }
            catch (Exception ex)
            {
                return new Joint();
            }
        }

        public IJoint DeepFind(JointType jt)
        {
            throw new NotImplementedException();
        }

        public bool Update(JointType jt, IJoint j)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IJoint other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Joint";
        }
    }
}
