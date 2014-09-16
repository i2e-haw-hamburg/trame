using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;

namespace TrameUnitTest
{
    [TestClass]
    public class JointTest
    {
        ITrame trame = new Trame.Trame();

        [TestMethod]
        public void CheckNormal()
        {
            ISkeleton s = trame.GetSkeleton();
            IJoint j = s.GetRoot();
            Assert.AreEqual(j.GetNormal(), null);
        }

        [TestMethod]
        public void CheckPoint()
        {
            ISkeleton s = trame.GetSkeleton();
            IJoint j = s.GetRoot();
            Assert.AreEqual(j.GetPoint(), null);
        }
    }
}
