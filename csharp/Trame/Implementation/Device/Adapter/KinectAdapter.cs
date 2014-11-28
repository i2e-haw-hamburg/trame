using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Trame.Implementation.Device.Adapter
{
    class KinectAdapter
    {
        private KinectSensor kinect = null;
        private BodyFrameReader reader = null;

        public void StartKinect(EventHandler<BodyFrameArrivedEventArgs> onFrameArrived)
        {
            kinect = KinectSensor.GetDefault();
            kinect.Open();
            reader = kinect.BodyFrameSource.OpenReader();
            reader.FrameArrived += onFrameArrived; 
        }

        public void StopKinect()
        {
            this.kinect.Close();
            this.kinect = null;
        }
    }
}
