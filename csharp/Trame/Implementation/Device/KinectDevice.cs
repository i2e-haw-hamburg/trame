using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
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
        private Thread t;
        private bool running = true;
        
        private void Run()
        {
            while (running)
            {
                Thread.Sleep(500);
            }
        }
        public KinectDevice()
        {
            adapter.StartKinect(OnFrameArrived);
            lastSkeleton = Skeleton.Creator.GetNewDefaultSkeleton();
            t = new Thread(Run);
            t.Start();
        }

        public ISkeleton GetSkeleton()
        {
            return lastSkeleton;
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            return lastSkeleton;
        }

        void OnFrameArrived(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                {
                    return;
                }
                if (this.foundedSkeletons == null)
                {
                    this.foundedSkeletons = new Microsoft.Kinect.Skeleton[frame.SkeletonArrayLength];
                }
                frame.CopySkeletonDataTo(foundedSkeletons);

                Microsoft.Kinect.Skeleton initSkeleton = this.foundedSkeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);
                if (initSkeleton != null)
                {
                    lastSkeleton = CreateSkeleon(initSkeleton);
                    FireNewSkeleton(lastSkeleton);
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
            var leftHand = initSkeleton.Joints[Microsoft.Kinect.JointType.HandLeft];
            var rightHand = initSkeleton.Joints[Microsoft.Kinect.JointType.HandRight];
            var leftHip = initSkeleton.Joints[Microsoft.Kinect.JointType.HipLeft];
            var rightHip = initSkeleton.Joints[Microsoft.Kinect.JointType.HipRight];
            var leftKnee = initSkeleton.Joints[Microsoft.Kinect.JointType.KneeLeft];
            var rightKnee = initSkeleton.Joints[Microsoft.Kinect.JointType.KneeRight];
            var leftAnkle = initSkeleton.Joints[Microsoft.Kinect.JointType.AnkleLeft];
            var rightAnkle = initSkeleton.Joints[Microsoft.Kinect.JointType.AnkleRight];
            var leftFoot = initSkeleton.Joints[Microsoft.Kinect.JointType.FootLeft];
            var rightFoot = initSkeleton.Joints[Microsoft.Kinect.JointType.FootRight];

            // left arm
            var lHand = new Skeleton.OrientedJoint
            {
                JointType = JointType.WRIST_LEFT,
                Point = AbsoluteToRelative(leftElbow.Position, leftWrist.Position),
                Valid = true
            };
            lHand.AddChild(new Skeleton.OrientedJoint { JointType = JointType.HAND_LEFT, Point = AbsoluteToRelative(leftWrist.Position, leftHand.Position) });
            var leftUnderArm = Skeleton.Creator.CreateParent(new List<IJoint>{lHand});
            leftUnderArm.JointType = JointType.ELBOW_LEFT;
            leftUnderArm.Point = AbsoluteToRelative(leftShoulder.Position, leftElbow.Position);
            leftUnderArm.Valid = true;
            var leftArm = Skeleton.Creator.CreateParent(new List<IJoint>{leftUnderArm});
            leftArm.JointType = JointType.SHOULDER_LEFT;
            leftArm.Point = AbsoluteToRelative(neck.Position, leftShoulder.Position);
            leftArm.Valid = true;

            // right arm
            var rightArm = new Skeleton.OrientedJoint { JointType = JointType.SHOULDER_RIGHT, Point = AbsoluteToRelative(neck.Position, rightShoulder.Position), Valid = true };
            rightArm.Append(
                new Skeleton.OrientedJoint { JointType = JointType.ELBOW_RIGHT, Point = AbsoluteToRelative(rightShoulder.Position, rightElbow.Position), Valid = true }
            ).Append(
                new Skeleton.OrientedJoint { JointType = JointType.WRIST_RIGHT, Point = AbsoluteToRelative(rightElbow.Position, rightWrist.Position), Valid = true }
            ).AddChild(new Skeleton.OrientedJoint { JointType = JointType.HAND_RIGHT, Point = AbsoluteToRelative(rightWrist.Position, rightHand.Position) });

            // left leg
            var leftLeg = new Skeleton.OrientedJoint { JointType = JointType.HIP_LEFT, Point = AbsoluteToRelative(spine.Position, leftHip.Position), Valid = true };
            leftLeg.Append(
                new Skeleton.OrientedJoint { JointType = JointType.KNEE_LEFT, Point = AbsoluteToRelative(leftHip.Position, leftKnee.Position), Valid = true }
            ).Append(
                new Skeleton.OrientedJoint { JointType = JointType.ANKLE_LEFT, Point = AbsoluteToRelative(leftKnee.Position, leftAnkle.Position), Valid = true }
            ).AddChild(new Skeleton.OrientedJoint { JointType = JointType.FOOT_LEFT, Point = AbsoluteToRelative(leftAnkle.Position, leftFoot.Position) });

            // right leg
            var rightLeg = new Skeleton.OrientedJoint { JointType = JointType.HIP_RIGHT, Point = AbsoluteToRelative(spine.Position, rightHip.Position), Valid = true };
            rightLeg.Append(
                new Skeleton.OrientedJoint { JointType = JointType.KNEE_RIGHT, Point = AbsoluteToRelative(rightHip.Position, rightKnee.Position), Valid = true }
            ).Append(
                new Skeleton.OrientedJoint { JointType = JointType.ANKLE_RIGHT, Point = AbsoluteToRelative(rightKnee.Position, rightAnkle.Position), Valid = true }
            ).AddChild(new Skeleton.OrientedJoint { JointType = JointType.FOOT_RIGHT, Point = AbsoluteToRelative(rightAnkle.Position, rightFoot.Position) });

            
            var jsNeck = Skeleton.Creator.CreateParent(new List<IJoint> { 
                leftArm,
                rightArm, 
                new Skeleton.OrientedJoint { JointType = JointType.HEAD, Point = AbsoluteToRelative(neck.Position, head.Position), Valid = true }
            });
            jsNeck.JointType = JointType.NECK;
            jsNeck.Point = AbsoluteToRelative(spine.Position, neck.Position);
            jsNeck.Valid = true;

            s.UpdateSkeleton(JointType.NECK, jsNeck);
            s.UpdateSkeleton(JointType.HIP_LEFT, leftLeg);
            s.UpdateSkeleton(JointType.HIP_RIGHT, rightLeg);
            s.Valid = true;
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

        public void Stop()
        {
            this.adapter.StopKinect();
            running = false;
            t.Join();
        }

        private void FireNewSkeleton(ISkeleton s)
        {
            if (NewSkeleton != null)
            {
                NewSkeleton(s);
            }
        }

        public event Action<ISkeleton> NewSkeleton;
    }
}
