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
        bool isValid = false;
        JointType type;

        public Joint()
        {
            normal = new Vector3();
            point = new Vector3();
            type = JointType.UNSPECIFIED;
        }

        public IList<IJoint> GetChildren()
        {
            return children.Select(x => x.Value).ToList();
        }
               
        public bool AddChild(IJoint j)
        {
            try
            {
                children.Add(j.JointType, j);
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
            IJoint j = FindChild(jt);
            if (j.JointType != jt)
            {
                foreach (var child in children)
                {
                    j = child.Value.DeepFind(jt);
                    if (j.JointType == jt)
                    {
                        break;
                    }
                }
            }

            return j;
        }

        public void Update(JointType jt, IJoint j)
        {
            if (FindChild(jt).JointType == jt)
            {
                foreach (var child in children)
                {
                    child.Value.Update(jt, j);
                }
            }
            else
            {
                RemoveChild(jt);
                AddChild(j);
            }
        }

        public bool Equals(IJoint o)
        {
            foreach(var child in children) {
                IJoint oc = o.FindChild(child.Value.JointType);
                if (!oc.Equals(child))
                {
                    return false;
                }
            }

            return isValid == o.Valid && type == o.JointType 
                && normal.Equals(o.Normal) && point.Equals(o.Point);
        }

        public override string ToString()
        {
            return string.Format("type:{0}, valid:{1}, point:{2}, children:[ {3} ]", type, isValid, point, string.Join(",", children.ToArray()));
        }


        public Vector3 Normal
        {
            get
            {
                return normal;
            }
            set
            {
                normal = value;
            }
        }

        public Vector3 Point
        {
            get
            {
                return point;
            }
            set
            {
                point = value;
            }
        }

        public JointType JointType
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public bool Valid
        {
            get
            {
                return isValid;
            }
            set
            {
                isValid = value;
            }
        }
    }
}
