using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Math;
using Microsoft.Kinect;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation;

namespace Trame.Implementation.Device
{
    class KinectDevice : IDevice
    {
        readonly KinectAdapter adapter = new KinectAdapter();

        private Microsoft.Kinect.Skeleton[] foundedSkeletons;
        private ISkeleton lastSkeleton;

        public KinectDevice()
        {
            adapter.StartKinect(OnFrameArrived);
            lastSkeleton = Skeleton.Creator.GetNewDefaultSkeleton();
        }

        
        public ISkeleton GetSkeleton()
        {
            return lastSkeleton;
        }

        void OnFrameArrived(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (this.foundedSkeletons == null)
                {
                    this.foundedSkeletons = new Microsoft.Kinect.Skeleton[frame.SkeletonArrayLength];
                }
                frame.CopySkeletonDataTo(foundedSkeletons);

                Microsoft.Kinect.Skeleton initSkeleton = this.foundedSkeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);
                if (initSkeleton != null)
                {
                    lastSkeleton = CreateSkeleon(initSkeleton);
                }
            }
        }

        private ISkeleton CreateSkeleon(Microsoft.Kinect.Skeleton initSkeleton)
        {
            var s = Skeleton.Creator.GetNewDefaultSkeleton();

            var neck = initSkeleton.Joints[Microsoft.Kinect.JointType.ShoulderCenter];
            var spine = initSkeleton.Joints[Microsoft.Kinect.JointType.Spine];
            var head = initSkeleton.Joints[Microsoft.Kinect.JointType.Head];
            var leftShoulder = initSkeleton.Joints[Microsoft.Kinect.JointType.ShoulderLeft];
            var rightShoulder = initSkeleton.Joints[Microsoft.Kinect.JointType.ShoulderRight];
            var leftElbow = initSkeleton.Joints[Microsoft.Kinect.JointType.ElbowLeft];
            var rightElbow = initSkeleton.Joints[Microsoft.Kinect.JointType.ElbowRight];
            var leftWrist = initSkeleton.Joints[Microsoft.Kinect.JointType.WristLeft];
            var rightWrist = initSkeleton.Joints[Microsoft.Kinect.JointType.WristRight];

            // left arm
            var leftUnderArm = Skeleton.Creator.CreateParent(new List<IJoint>
            {
               new Skeleton.Joint { JointType = JointType.WRIST_LEFT, Point = AbsoluteToRelative(leftElbow.Position, leftWrist.Position), Valid = true }
            });
            leftUnderArm.JointType = JointType.ELBOW_LEFT;
            leftUnderArm.Point = AbsoluteToRelative(leftShoulder.Position, leftElbow.Position);
            leftUnderArm.Valid = true;

            var leftArm = Skeleton.Creator.CreateParent(new List<IJoint>{leftUnderArm});
            leftArm.JointType = JointType.SHOULDER_LEFT;
            leftArm.Point = AbsoluteToRelative(neck.Position, leftShoulder.Position);
            leftArm.Valid = true;

            // right arm
            var rightUnderArm = Skeleton.Creator.CreateParent(new List<IJoint>
            {
               new Skeleton.Joint { JointType = JointType.WRIST_RIGHT, Point = AbsoluteToRelative(rightElbow.Position, rightWrist.Position), Valid = true }
            });
            rightUnderArm.JointType = JointType.ELBOW_RIGHT;
            rightUnderArm.Point = AbsoluteToRelative(rightShoulder.Position, rightElbow.Position);
            rightUnderArm.Valid = true;
            var rightArm = Skeleton.Creator.CreateParent(new List<IJoint>{rightUnderArm});
            rightArm.JointType = JointType.SHOULDER_RIGHT;
            rightArm.Point = AbsoluteToRelative(neck.Position, rightShoulder.Position);
            rightArm.Valid = true;

            var jsNeck = Skeleton.Creator.CreateParent(new List<IJoint> { 
                leftArm,
                rightArm, 
                new Skeleton.Joint { JointType = JointType.HEAD, Point = AbsoluteToRelative(neck.Position, head.Position), Valid = true }
            });
            jsNeck.JointType = JointType.NECK;
            jsNeck.Point = AbsoluteToRelative(spine.Position, neck.Position);
            jsNeck.Valid = true;

            s.UpdateSkeleton(JointType.NECK, jsNeck);

            return s;
        }

        private static Vector3 AbsoluteToRelative(SkeletonPoint parent, SkeletonPoint child)
        {
            return new Vector3(
                child.X-parent.X,
                child.Y-parent.Y,
                child.Z-parent.Z
            ) * 1000;
        }
    }
}
