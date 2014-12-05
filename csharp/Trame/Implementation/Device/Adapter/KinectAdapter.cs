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

        public void StartKinect(EventHandler<SkeletonFrameReadyEventArgs> onFrameArrived)
        {
            var pot = KinectSensor.KinectSensors[0];
            if (pot.Status == KinectStatus.Connected)
            {
                kinect = pot;
            }
            else
            {
                throw new Exception("No Kinect is connected");
            }
            kinect.SkeletonFrameReady += onFrameArrived;
            kinect.Start();
            kinect.SkeletonStream.Enable();
        }

        public void StopKinect()
        {
            this.kinect.Stop();
            this.kinect = null;
        }
    }
}
