using System;
using System.Collections.Generic;
using TrameSkeleton.Interface;


namespace Trame.Implementation.Skeleton
{
    [Serializable]
    public class Skeleton : ISkeleton
    {
        IJoint root;
        bool valid;
        uint id;
        uint timestamp;
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Skeleton.Skeleton"/> class.
		/// </summary>
        public Skeleton()
            : this(0, false, (uint)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond))
        {
            
        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Skeleton.Skeleton"/> class.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="valid">If set to <c>true</c> valid.</param>
		/// <param name="timestamp">Timestamp.</param>
        public Skeleton(uint id, bool valid, uint timestamp)
        {
            this.valid = valid;
            this.id = id;
            this.timestamp = timestamp;
        }

        public void UpdateSkeleton(JointType jt, IJoint j)
        {
            root.Update(jt, j);
        }

        public IJoint GetJoint(JointType jt)
        {
            return root.DeepFind(jt);
        }
		/// <summary>
		/// Determines whether the specified <see cref="Trame.ISkeleton"/> is equal to the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.
		/// </summary>
		/// <param name="other">The <see cref="Trame.ISkeleton"/> to compare with the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="Trame.ISkeleton"/> is equal to the current
		/// <see cref="Trame.Implementation.Skeleton.Skeleton"/>; otherwise, <c>false</c>.</returns>
        public bool Equals(ISkeleton other)
        {
            return valid == other.Valid && root.Equals(other.Root);
        }
		/// <summary>
		/// Returns a <see cref="System.String"/> that represents the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.
		/// </summary>
		/// <returns>A <see cref="System.String"/> that represents the current <see cref="Trame.Implementation.Skeleton.Skeleton"/>.</returns>
        public override string ToString()
        {
            return string.Format("id:{0}, valid:{1}, timestamp:{2}, root:{3}", id, valid, timestamp, root);
        }


        public IJoint Root { get; set; }

        public uint Timestamp { get; }

        public uint ID { get; set; }

        public bool Valid { get; set; }

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
            var s = new Skeleton(id, valid, timestamp);
            s.root = root.Clone();
            return s;
        }

        public IJoint GetHead()
        {
            return Root.DeepFind(JointType.HEAD);
        }

        public IHand GetHand(HandType type, bool preferRight = true)
        {
            return null;
        }

        public IList<IJoint> Joints
        {
            get
            {
                return new List<IJoint>{Root};
            }
        }
    }
}
