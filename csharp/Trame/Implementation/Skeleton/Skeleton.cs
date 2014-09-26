using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trame.Implementation.Skeleton
{
    class Skeleton : ISkeleton
    {
        IJoint root;
        bool valid;
        uint id;
        uint timestamp;

        public Skeleton()
        {
            valid = false;
            id = 0;
            timestamp = (uint)(DateTime.Now.Ticks / TimeSpan.TicksPerSecond);
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
            return "Skelton";
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
    }
}
