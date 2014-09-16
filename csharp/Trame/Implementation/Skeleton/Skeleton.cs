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


        public bool UpdateSkeleton(JointType jt, IJoint j)
        {
            throw new NotImplementedException();
        }

        public IJoint GetJoint(JointType jt)
        {
            throw new NotImplementedException();
        }

        public IJoint GetRoot()
        {
            throw new NotImplementedException();
        }

        public uint GetTimestamp()
        {
            throw new NotImplementedException();
        }

        public uint GetID()
        {
            throw new NotImplementedException();
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
        }

        public bool Equals(ISkeleton other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Skelton";
        }
    }
}
