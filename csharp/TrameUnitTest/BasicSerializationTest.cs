using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trame;
using AForge.Math;
using Trame.Implementation.Skeleton;
using TrameSerialization;

namespace TrameUnitTest
{
    [TestClass]
    public class BasicSerializationTest
    {
        Serialization serialization = new Serialization(OutputType.BASIC);

        [TestMethod]
        public void CheckOutputType()
        {
            Assert.AreEqual(serialization.OutputType, OutputType.BASIC);
        }

        [TestMethod]
        public void CheckStreamData()
        {
            var skeleton = Creator.GetNewInvalidSkeleton();
            using (var stream = serialization.Serialize(skeleton))
            {
                var ms = new MemoryStream();
                stream.CopyTo(ms);
                Assert.IsTrue(ms.Length > 0);
            }
        }

        [TestMethod]
        public void CheckDeserialization()
        {
            var skeleton = Creator.GetNewInvalidSkeleton();
            using (var stream = serialization.Serialize(skeleton))
            {
                var s = serialization.Deserialize(stream);
                Assert.AreEqual(skeleton.ID, s.ID);
                Assert.AreEqual(skeleton.Valid, s.Valid);
                Assert.AreEqual(skeleton.Timestamp, s.Timestamp);
                Assert.AreEqual(skeleton, s);
            }
        }
    }
}
