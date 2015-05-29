using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Trame.Implementation.Device.Adapter
{
	/// <summary>
	/// Kinect adapter.
	/// </summary>
    class KinectAdapter
    {
        private KinectSensor kinect = null;
		/// <summary>
		/// Starts the kinect.
		/// </summary>
		/// <param name="onFrameArrived">On frame arrived.</param>
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
            kinect.SkeletonStream.Enable(new TransformSmoothParameters()
            {
                Smoothing = 0.75f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 0.05f,
                MaxDeviationRadius = 0.04f
            });
        }
		/// <summary>
		/// Stops the kinect.
		/// </summary>
        public void StopKinect()
        {
            this.kinect.Stop();
            this.kinect = null;
        }
    }
}
