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
    public class OrientedJoint<K, T> : IJoint<K, T> where K : new() where T : new()
    {
        IDictionary<JointType, IJoint<K,T>> children = new Dictionary<JointType, IJoint<K,T>>();
        K orientation;
        T point;
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
            orientation = new K();
            point = new T();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<IJoint<K, T>> GetChildren()
        {
            return children.Select(x => x.Value).ToList();
        }
              
        /// <summary>
        /// 
        /// </summary>
        /// <param name="j"></param>
        /// <returns></returns>
        public bool AddChild(IJoint<K, T> j)
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
        public void AddChildren(IEnumerable<IJoint<K, T>> joints)
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
        public IJoint<K, T> FindChild(JointType jt)
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
                return new OrientedJoint<K, T>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jt"></param>
        /// <returns></returns>
        public IJoint<K, T> DeepFind(JointType jt)
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
        public void Update(JointType jt, IJoint<K, T> j)
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
        public bool Equals(IJoint<K, T> o)
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
        public K Orientation
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
        public T Point
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


        public IJoint<K, T> Append(IJoint<K, T> j)
        {
            AddChild(j);
            return j;
        }

        public IJoint<K, T> Clone()
        {
            var j = new OrientedJoint<K, T>(JointType, isValid) { Point = Point, Orientation = Orientation };
            j.AddChildren(j.GetChildren());
            return j;
        }
    }
}
