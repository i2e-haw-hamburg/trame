using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;

namespace TrameUnitTest
{
    [TestClass]
    public class SkeletonTest
    {
        ICameraAbstraction trame = null;

            
        [TestMethod]
        public void TestMethod1()
        {
            trame = new Trame.Trame();
        }
    }
}
