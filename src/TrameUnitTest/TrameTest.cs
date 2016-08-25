using System;
using Trame.Interface;

namespace TrameUnitTest
{
    using NUnit.Framework;


    [TestFixture]
    public class TrameTest
    {
        [Test]
        public void TestDeviceInjection()
        {
            var trame = new Trame.Trame(new DeviceMock());

        }
    }

    class DeviceMock : IDevice
    {
        public ISkeleton GetSkeleton()
        {
            return null;
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            return null;
        }

        public void Stop()
        {
            
        }

        public void Start()
        {
            
        }

        public event Action<ISkeleton> NewSkeleton;
    }
}