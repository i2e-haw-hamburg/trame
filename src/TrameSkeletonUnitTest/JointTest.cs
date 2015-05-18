using System;
using System.Collections.Generic;
using AForge.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;
using Trame.Implementation.Skeleton;

namespace TrameUnitTest
{
    [TestClass]
    public class JointTest
    {
        private IJoint<Vector4, Vector3> root;

        [TestInitialize()]
        public void Initialize()
        {
            root = Creator.GetNewDefaultSkeleton().Root;
        }

        [TestMethod]
        public void TestFindChild()
        {
            var neck = root.FindChild(JointType.NECK);

            Assert.AreEqual(JointType.NECK, neck.JointType);
            Assert.AreEqual(3, neck.GetChildren().Count);
            // with simple find, only the first generation will be searched - result should be an unspecified element
            var head = root.FindChild(JointType.HEAD);
            Assert.AreEqual(JointType.UNSPECIFIED, head.JointType);
            Assert.AreEqual(false, head.Valid);
            // search over more then one step with find
            head = neck.FindChild(JointType.HEAD);
            Assert.AreEqual(JointType.HEAD, head.JointType);
            Assert.AreEqual(0, head.GetChildren().Count);
            Assert.AreEqual(true, head.Valid);
        }

        [TestMethod]
        public void TestDeepFind()
        {
            var neck = root.DeepFind(JointType.NECK);
            Assert.AreEqual(JointType.NECK, neck.JointType);
            Assert.AreEqual(3, neck.GetChildren().Count);

            var head = root.DeepFind(JointType.HEAD);
            Assert.AreEqual(JointType.HEAD, head.JointType);
            Assert.AreEqual(0, head.GetChildren().Count);
            Assert.AreEqual(true, head.Valid);

            var elbowLeft = root.DeepFind(JointType.ELBOW_LEFT);
            Assert.AreEqual(JointType.ELBOW_LEFT, elbowLeft.JointType);
            Assert.AreEqual(1, elbowLeft.GetChildren().Count);
            Assert.AreEqual(true, elbowLeft.Valid);

            var kneeLeft = neck.DeepFind(JointType.KNEE_LEFT);
            Assert.AreEqual(JointType.UNSPECIFIED, kneeLeft.JointType);
            Assert.AreEqual(0, kneeLeft.GetChildren().Count);
            Assert.AreEqual(false, kneeLeft.Valid);
        }

        [TestMethod]
        public void TestUpdate()
        {

        }
        
    }
}
