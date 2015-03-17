﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;
using AForge.Math;

namespace TrameUnitTest
{
    [TestClass]
    public class JointTest
    {
        ICameraAbstraction trame = null;

        [TestMethod]
        public void CheckNormal()
        {
            trame = new Trame.Trame();
            trame.NewSkeleton += s =>
            {
                IJoint j = s.Root;
                Assert.AreEqual(new Vector3(0, 0, -100), j.Normal);
            };

        }

        [TestMethod]
        public void CheckPoint()
        {
            trame = new Trame.Trame();
            // wait until 
            trame.NewSkeleton += s =>
            {
                IJoint j = s.Root;
                Assert.AreEqual(new Vector3(0, 1000, 0), j.Point);
            };

        }
    }
}