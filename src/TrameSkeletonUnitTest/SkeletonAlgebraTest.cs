using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Implementation;
using TrameSkeleton.Math;

namespace TrameSkeletonUnitTest
{
    [TestClass]
    public class SkeletonAlgebraTest
    {
        [TestMethod]
        public void TestDiff()
        {
            var s1 = Creator.GetNewDefaultSkeleton();
            var s2 = Creator.GetNewDefaultSkeleton();

            var diff = SkeletonAlgebra.Diff(s1, s2);
            
            Assert.IsInstanceOfType(diff, typeof (SkeletonDiff));
            Assert.AreEqual(new Vector3(0, 0, 0), diff.Root.Point);
            ChildRecursive(diff.Root, x => new Vector3(0, 0, 0));
            
        }

        private void ChildRecursive(IJoint child, Func<IJoint, Vector3> same)
        {
            foreach (var c in child.GetChildren())
            {
                Assert.AreEqual(same(c), c.Point);
                ChildRecursive(c, same);
            }
        }

        [TestMethod]
        public void TestAdd()
        {
            var s1 = Creator.GetNewDefaultSkeleton();
            var s2 = Creator.GetNewDefaultSkeleton();

            var diff = SkeletonAlgebra.Diff(s1, s2);

            Assert.IsInstanceOfType(diff, typeof(SkeletonDiff));
            var s3 = SkeletonAlgebra.Add(s1, diff);

            ChildRecursive(diff.Root, x => s1.GetJoint(x.JointType).Point);

        }
    }
}
