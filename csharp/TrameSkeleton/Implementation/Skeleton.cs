using System;
using NetworkMessages.Trame;

namespace Trame.Implementation.Skeleton
{
    [Serializable]
    public class Skeleton : ISkeleton
    {
        IJoint root;
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

        public void UpdateSkeleton(JointType jt, IJoint j)
        {
            root.Update(jt, j);
        }

        public IJoint GetJoint(JointType jt)
        {
            return root.DeepFind(jt);
        }
        
        public bool Equals(ISkeleton other)
        {
            return valid == other.Valid && root.Equals(other.Root);
        }

        public override string ToString()
        {
            return string.Format("id:{0}, valid:{1}, timestamp:{2}, root:{3}", id, valid, timestamp, root);
        }


        public IJoint Root
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
        
        public ISkeleton GetArms()
        {
            var r = Root.FindChild(JointType.NECK);

            r.RemoveChild(JointType.HEAD);
            r.Normal = Root.Normal;
            r.Point += Root.Point;
            
            Root = r;
            return this;
        }

        public static ISkeleton FromMessage(SkeletonMessage message)
        {
            var skeleton = new Skeleton((uint)message.id, message.valid, (uint)message.timestamp);
            skeleton.Root = Joint.FromMessage(message.root);

            return skeleton;
        }

        public SkeletonMessage ToMessage()
        {
            var message = new SkeletonMessage();
            message.id = id;
            message.timestamp = timestamp;
            message.valid = valid;
            message.root = root.ToMessage();

            return message;
        }
    }
}
