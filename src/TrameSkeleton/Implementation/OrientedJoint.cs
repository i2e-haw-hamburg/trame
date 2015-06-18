using System;
using System.Collections.Generic;
using System.Linq;
using TrameSkeleton.Math;


namespace Trame.Implementation.Skeleton
{
    /// <summary>
    /// Oriented joint.
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
        /// Initializes a new instance of the <see cref="Trame.Implementation.Skeleton.OrientedJoint"/> class.
        /// </summary>
        public OrientedJoint() : this(JointType.UNSPECIFIED, false)
        {}
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Skeleton.OrientedJoint"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="valid">If set to <c>true</c> valid.</param>
        public OrientedJoint(JointType type, bool valid)
        {
            this.type = type;
            this.isValid = valid;
            orientation = new Vector4();
            point = new Vector3();
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

        public void AddChildren(IEnumerable<IJoint> joints)
        {
            foreach (var joint in joints)
            {
                AddChild(joint);
            }
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
                return new OrientedJoint();
            }
        }

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
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Trame.Implementation.Skeleton.OrientedJoint"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Trame.Implementation.Skeleton.OrientedJoint"/>.</returns>
        public override string ToString()
        {
            return string.Format("type:{0}, valid:{1}, point:{2}, children:[ {3} ]", type, isValid, point, string.Join(",", children.Select(x => x.ToString()).ToArray()));
        }

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
