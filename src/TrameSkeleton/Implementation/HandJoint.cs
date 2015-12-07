using Trame;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Interface;

namespace TrameSkeleton.Implementation
{
    public class HandJoint : OrientedJoint, IHand
    {
        public bool IsClosed { get; set; }

        public Side Side { get; set; }

        public HandJoint(bool isClosed, Side side)
        {
            IsClosed = isClosed;
            Side = side;
        }

        public HandJoint(JointType type, bool valid, bool isClosed, Side side)
            : base(type, valid)
        {
            IsClosed = isClosed;
            Side = side;
        }

        public HandJoint()
        {
        }
    }
}
