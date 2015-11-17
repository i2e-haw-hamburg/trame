extern alias KinectV2;
using System;
using KinectV2::Microsoft.Kinect;

namespace Trame.Implementation.Device
{
    extern alias KinectV1;

    internal class KinectV2Device : IDevice
    {
        private KinectV2::Microsoft.Kinect.KinectSensor _kinectSensor;
        private KinectV2::Microsoft.Kinect.CoordinateMapper _coordinateMapper;
        private int _displayWidth;
        private int _displayHeight;
        private KinectV2::Microsoft.Kinect.BodyFrameReader _bodyFrameReader;

        public KinectV2Device()
        {
            // one sensor is currently supported
            _kinectSensor = KinectV2::Microsoft.Kinect.KinectSensor.GetDefault();

            // get the coordinate mapper
            _coordinateMapper = _kinectSensor.CoordinateMapper;

            // get the depth (display) extents
            KinectV2::Microsoft.Kinect.FrameDescription frameDescription = _kinectSensor.DepthFrameSource.FrameDescription;

            // get size of joint space
            _displayWidth = frameDescription.Width;
            _displayHeight = frameDescription.Height;

            // open the reader for the body frames
            _bodyFrameReader = _kinectSensor.BodyFrameSource.OpenReader();
        }

        public ISkeleton GetSkeleton()
        {
            throw new NotImplementedException();
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public event Action<ISkeleton> NewSkeleton;
    }
}
