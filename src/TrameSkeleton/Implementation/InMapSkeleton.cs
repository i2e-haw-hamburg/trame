using System;
using System.Collections.Generic;
using System.Linq;


namespace Trame.Implementation.Skeleton
{
    [Serializable]
    public class InMapSkeleton : ISkeleton
    {
        private IDictionary<JointType, IJoint> _joints; 
        bool valid;
        uint id;
        uint timestamp;
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Skeleton.Skeleton"/> class.
		/// </summary>
        public InMapSkeleton()
            : this(0, false, (uint)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond))
        {
            
        }
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Skeleton.Skeleton"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="valid">If set to <c>true</c> valid.</param>
		/// <param name="timestamp">Timestamp.</param>
        public InMapSkeleton(uint id, bool valid, uint timestamp)
        {
            this.valid = valid;
            this.id = id;
            this.timestamp = timestamp;
            _joints = new Dictionary<JointType, IJoint>();
        }

        public void UpdateSkeleton(JointType jt, IJoint j)
        {
            lock (_joints)
            {
                _joints[jt] = j;
            }
        }

        public void Add(IJoint j)
        {
            UpdateSkeleton(j.JointType, j);
        }

        public IJoint GetJoint(JointType jt)
        {
            return _joints[jt];
        }
		/// <summary>
		/// Determines whether the specified <see cref="Trame.ISkeleton"/> is equal to the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.
		/// </summary>
		/// <param name="other">The <see cref="Trame.ISkeleton"/> to compare with the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Trame.ISkeleton"/> is equal to the current
		/// <see cref="Trame.Implementation.Skeleton.Skeleton"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(ISkeleton other)
        {
            return valid == other.Valid && _joints.Values.Equals(other.Joints);
        }
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.</returns>
        public override string ToString()
        {
            return $"id:{id}, valid:{valid}, timestamp:{timestamp}, root:{_joints}";
        }


        public IJoint Root
        {
            get
            {
                return _joints[JointType.CENTER];
            }
            set
            {
                lock (_joints)
                {
                    _joints[JointType.CENTER] = value;
                }
            }
        }

        public uint Timestamp => timestamp;

        public uint ID
        {
            get
            {
                return id;
            }
            set { id = value; }
        }

        public bool Valid
        {
            get
            {
                return valid;
            }
            set
            {
                valid = value;
            }
        }

        public ISkeleton GetArms()
        {
            var s = Clone();
            var r = s.Root.FindChild(JointType.NECK);

            r.RemoveChild(JointType.HEAD);
            r.Orientation = Root.Orientation;
            r.Point = Root.Point;

            s.Root = r;
            return s;
        }

        public ISkeleton Clone()
        {
            var s = new InMapSkeleton(id, valid, timestamp);
            lock (_joints)
            {
                foreach (var key in _joints.Keys)
                {
                    s._joints.Add(key, _joints[key]);
                }
            }

            return s;
        }

        public IJoint GetHead()
        {
            return _joints[JointType.HEAD];
        }

        public IList<IJoint> Joints
        {
            get
            {
                lock (_joints)
                {
                    return _joints.Select(e => e.Value).ToList();
                }
            }
        }
    }
}
