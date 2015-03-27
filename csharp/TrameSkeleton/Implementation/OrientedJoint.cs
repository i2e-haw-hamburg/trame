using AForge.Math;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Trame.Implementation.Skeleton
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable()]
    public class OrientedJoint : IJoint
    {
        IDictionary<JointType, IJoint> children = new Dictionary<JointType, IJoint>();
        Vector4 orientation;
        Vector3 point;
        bool isValid;
        JointType type;

        /// <summary>
        /// 
        /// </summary>
        public OrientedJoint() : this(JointType.UNSPECIFIED, false)
        {}

        public OrientedJoint(JointType type, bool valid)
        {
            this.type = type;
            this.isValid = valid;
            orientation = new Vector4();
            point = new Vector3();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IJoint> GetChildren()
        {
            return children.Select(x => x.Value).ToList();
        }
              
        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="joints"></param>
        public void AddChildren(IEnumerable<IJoint> joints)
        {
            foreach (var joint in joints)
            {
                AddChild(joint);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
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
                return new OrientedJoint();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        public IJoint DeepFind(JointType jt)
        {
            var j = FindChild(jt);
            if (j.JointType == jt)
            {
                return j;
            }
            var sorted = children.Keys.OrderByDescending(x => x);
            try
            {
                var key = sorted.First(x => x < jt);
                j = children[key].DeepFind(jt);
            }
            catch (InvalidOperationException)
            {}
            
            return j;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <param name="j"></param>
        public void Update(JointType jt, IJoint j)
        {
            if (FindChild(jt).JointType != jt)
            {
                var sorted = children.Keys.OrderByDescending(x => x);
                try
                {
                    var key = sorted.First(x => x < jt);
                    children[key].Update(jt, j);
                }
                catch (InvalidOperationException)
                { }
            }
            else
            {
                RemoveChild(jt);
                AddChild(j);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Equals(IJoint o)
        {
            foreach(var child in children) {
                var oc = o.FindChild(child.Value.JointType);
                if (!oc.Equals(child.Value))
                {
                    return false;
                }
            }

            return isValid == o.Valid && type == o.JointType 
                && orientation.Equals(o.Orientation) && point.Equals(o.Point);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("type:{0}, valid:{1}, point:{2}, children:[ {3} ]", type, isValid, point, string.Join(",", children.Select(x => x.ToString()).ToArray()));
        }

        /// <summary>
        /// 
        /// </summary>
        public Vector4 Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                orientation = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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


        public IJoint Append(IJoint j)
        {
            AddChild(j);
            return j;
        }

        public IJoint Clone()
        {
            var j = new OrientedJoint(JointType, isValid) { Point = Point, Orientation = Orientation };
            j.AddChildren(j.GetChildren());
            return j;
        }
    }
}
