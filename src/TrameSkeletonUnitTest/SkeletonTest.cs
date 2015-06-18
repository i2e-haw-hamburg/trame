using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Math;

namespace TrameUnitTest
{
    [TestClass]
    public class SkeletonTest
    {
            
        [TestMethod]
        public void TestUpdate()
        {
            var s = Creator.GetNewDefaultSkeleton<InMapSkeleton>();
            var head = new OrientedJoint {JointType = JointType.HEAD, Point = new Vector3(1, 2, 3)};
            s.UpdateSkeleton(JointType.HEAD, head);

            var head2 = s.GetJoint(JointType.HEAD);
            Assert.AreEqual(head.Point, head2.Point);
            Assert.AreEqual(head, head2);

            Assert.AreEqual(3, s.Root.GetChildren().Count);
        }

        [TestMethod]
        public void TestEquals()
        {
            
        }

        [TestMethod]
        public void TestGetArms()
        {
            
        }

        [TestMethod]
        public void TestHead()
        {
            
        }
    }
}
