using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trame;
using Trame.Implementation.Skeleton;

namespace TrameSkeleton.Implementation
{
    public class HandJoint : OrientedJoint, IHand
    {
        public bool IsClosed { get; set; }
    }
}
