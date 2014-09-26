using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;

namespace TrameUnitTest
{
    [TestClass]
    public class JointTest
    {
        ICameraAbstraction trame = new Trame.Trame();

        [TestMethod]
        public void CheckNormal()
        {
            ISkeleton s = trame.GetSkeleton();
            IJoint j = s.Root;
            Assert.AreEqual(j.Normal, null);
        }

        [TestMethod]
        public void CheckPoint()
        {
            ISkeleton s = trame.GetSkeleton();
            IJoint j = s.Root;
            Assert.AreEqual(j.Point, null);
        }
    }
}
