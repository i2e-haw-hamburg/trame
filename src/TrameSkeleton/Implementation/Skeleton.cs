using System;


namespace Trame.Implementation.Skeleton
{
    [Serializable]
    public class Skeleton<K, T> : ISkeleton<K, T>
    {
        IJoint<K, T> root;
        bool valid;
        uint id;
        uint timestamp;

        public Skeleton()
            : this(0, false, (uint)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond))
        {
            
        }

        public Skeleton(uint id, bool valid, uint timestamp)
        {
            this.valid = valid;
            this.id = id;
            this.timestamp = timestamp;
        }

        public void UpdateSkeleton(JointType jt, IJoint<K, T> j)
        {
            root.Update(jt, j);
        }

        public IJoint<K, T> GetJoint(JointType jt)
        {
            return root.DeepFind(jt);
        }

        public bool Equals(ISkeleton<K, T> other)
        {
            return valid == other.Valid && root.Equals(other.Root);
        }

        public override string ToString()
        {
            return string.Format("id:{0}, valid:{1}, timestamp:{2}, root:{3}", id, valid, timestamp, root);
        }


        public IJoint<K, T> Root
        {
            get
            {
                return root;
            }
            set
            {
                root = value;
            }
        }

        public uint Timestamp
        {
            get
            {
                return timestamp;
            }
        }

        public uint ID
        {
            get
            {
                return id;
            }
            
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

        public ISkeleton<K, T> GetArms()
        {
            var r = Root.FindChild(JointType.NECK);

            r.RemoveChild(JointType.HEAD);
            r.Orientation = Root.Orientation;
            r.Point = Root.Point;
            
            Root = r;
            return this;
        }

        public ISkeleton<K, T> Clone()
        {
            var s = new Skeleton<K, T>(id, valid, timestamp);
            s.root = root.Clone();
            return s;
        }

        public IJoint<K, T> GetHead()
        {
            return Root.DeepFind(JointType.HEAD);
        }
    }
}
