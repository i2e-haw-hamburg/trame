using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device
{
    class KinectDevice : IDevice
    {
        readonly KinectAdapter adapter = new KinectAdapter();

        public KinectDevice()
        {
            adapter.StartKinect(OnFrameArrived);
        }
        
        public ISkeleton GetSkeleton()
        {

            ISkeleton s = Creator.GetNewDefaultSkeleton();

            return s;
        }

        void OnFrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            using (var frame = e.FrameReference.AcquireFrame())
            {
                if ((frame != null) && (frame.BodyCount > 0))
                {
                    Console.WriteLine("Bodies detected: " + frame.BodyCount);
                }
            }
        }  
    }
}
