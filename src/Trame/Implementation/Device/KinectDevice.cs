﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AForge.Math;
using Microsoft.Kinect;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;
using Vector4 = AForge.Math.Vector4;

namespace Trame.Implementation.Device
{
    internal class KinectDevice : IDevice
    {
        private readonly KinectAdapter adapter = new KinectAdapter();
        private readonly Thread t;
        private Microsoft.Kinect.Skeleton[] foundedSkeletons;
        private ISkeleton<Vector4, Vector3> lastSkeleton;
        private bool running = true;

        public KinectDevice()
        {
            adapter.StartKinect(OnFrameArrived);
            lastSkeleton = Creator.GetNewDefaultSkeleton();
            t = new Thread(Run);
            t.Start();
        }

        public ISkeleton<Vector4, Vector3> GetSkeleton()
        {
            return lastSkeleton;
        }

        public ISkeleton<Vector4, Vector3> GetSkeleton(ISkeleton<Vector4, Vector3> baseSkeleton)
        {
            return lastSkeleton;
        }

        public void Stop()
        {
            adapter.StopKinect();
            running = false;
            t.Join();
        }

        public event Action<ISkeleton<Vector4, Vector3>> NewSkeleton;

        private void Run()
        {
            while (running)
            {
                Thread.Sleep(500);
            }
        }

        private void OnFrameArrived(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                {
                    return;
                }
                if (foundedSkeletons == null)
                {
                    foundedSkeletons = new Microsoft.Kinect.Skeleton[frame.SkeletonArrayLength];
                }
                frame.CopySkeletonDataTo(foundedSkeletons);

                var initSkeleton = foundedSkeletons.FirstOrDefault(s => s.TrackingState == SkeletonTrackingState.Tracked);
                if (initSkeleton != null)
                {
                    lastSkeleton = CreateSkeleton(initSkeleton);
                    FireNewSkeleton(lastSkeleton);
                }
            }
        }

        private ISkeleton<Vector4, Vector3> CreateSkeleton(Microsoft.Kinect.Skeleton initSkeleton)
        {
            var s = Creator.GetNewDefaultSkeleton();

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

            var neckOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.ShoulderCenter].AbsoluteRotation.Quaternion;
            var spineOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.HipCenter].AbsoluteRotation.Quaternion;
            var headOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.Head].AbsoluteRotation.Quaternion;
            var leftShoulderOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.ShoulderLeft].AbsoluteRotation.Quaternion;
            var rightShoulderOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.ShoulderRight].AbsoluteRotation.Quaternion;
            var leftElbowOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.ElbowLeft].AbsoluteRotation.Quaternion;
            var rightElbowOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.ElbowRight].AbsoluteRotation.Quaternion;
            var leftWristOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.WristLeft].AbsoluteRotation.Quaternion;
            var rightWristOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.WristRight].AbsoluteRotation.Quaternion;
            var leftHandOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.HandLeft].AbsoluteRotation.Quaternion;
            var rightHandOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.HandRight].AbsoluteRotation.Quaternion;
            var leftHipOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.HipLeft].AbsoluteRotation.Quaternion;
            var rightHipOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.HipRight].AbsoluteRotation.Quaternion;
            var leftKneeOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.KneeLeft].AbsoluteRotation.Quaternion;
            var rightKneeOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.KneeRight].AbsoluteRotation.Quaternion;
            var leftAnkleOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.AnkleLeft].AbsoluteRotation.Quaternion;
            var rightAnkleOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.AnkleRight].AbsoluteRotation.Quaternion;
            var leftFootOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.FootLeft].AbsoluteRotation.Quaternion;
            var rightFootOrientation = initSkeleton.BoneOrientations[Microsoft.Kinect.JointType.FootRight].AbsoluteRotation.Quaternion;

            // left arm
            var lHand = new OrientedJoint<Vector4, Vector3>
            {
                JointType = JointType.WRIST_LEFT,
                Point = AbsoluteToRelative(leftElbow.Position, leftWrist.Position),
                Orientation = ToVec4(leftWristOrientation),
                Valid = true
            };
            lHand.AddChild(new OrientedJoint<Vector4, Vector3>
            {
                JointType = JointType.HAND_LEFT,
                Point = AbsoluteToRelative(leftWrist.Position, leftHand.Position),
                Orientation = ToVec4(leftHandOrientation),
            });
            var leftUnderArm = Creator.CreateParent(new List<IJoint<Vector4, Vector3>> {lHand});
            leftUnderArm.JointType = JointType.ELBOW_LEFT;
            leftUnderArm.Orientation = ToVec4(leftElbowOrientation);
            leftUnderArm.Point = AbsoluteToRelative(leftShoulder.Position, leftElbow.Position);
            leftUnderArm.Valid = true;
            var leftArm = Creator.CreateParent(new List<IJoint<Vector4, Vector3>> {leftUnderArm});
            leftArm.JointType = JointType.SHOULDER_LEFT;
            leftArm.Point = AbsoluteToRelative(neck.Position, leftShoulder.Position);
            leftArm.Orientation = ToVec4(leftShoulderOrientation);
            leftArm.Valid = true;

            // right arm
            var rightArm = new OrientedJoint<Vector4, Vector3>
            {
                JointType = JointType.SHOULDER_RIGHT,
                Orientation = ToVec4(rightShoulderOrientation),
                Point = AbsoluteToRelative(neck.Position, rightShoulder.Position),
                Valid = true
            };
            rightArm.Append(
                new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.ELBOW_RIGHT,
                    Point = AbsoluteToRelative(rightShoulder.Position, rightElbow.Position),
                    Orientation = ToVec4(rightElbowOrientation),
                    Valid = true
                }
                ).Append(
                    new OrientedJoint<Vector4, Vector3>
                    {
                        JointType = JointType.WRIST_RIGHT,
                        Point = AbsoluteToRelative(rightElbow.Position, rightWrist.Position),
                        Orientation = ToVec4(rightWristOrientation),
                        Valid = true
                    }
                )
                .AddChild(new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.HAND_RIGHT,
                    Point = AbsoluteToRelative(rightWrist.Position, rightHand.Position),
                    Orientation = ToVec4(rightHandOrientation),
                });

            // left leg
            var leftLeg = new OrientedJoint<Vector4, Vector3>
            {
                JointType = JointType.HIP_LEFT,
                Point = AbsoluteToRelative(spine.Position, leftHip.Position),
                 Orientation = ToVec4(leftKneeOrientation),
                Valid = true
            };
            leftLeg.Append(
                new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.KNEE_LEFT,
                    Point = AbsoluteToRelative(leftHip.Position, leftKnee.Position),
                     Orientation = ToVec4(leftAnkleOrientation),
                    Valid = true
                }
                ).Append(
                    new OrientedJoint<Vector4, Vector3>
                    {
                        JointType = JointType.ANKLE_LEFT,
                        Point = AbsoluteToRelative(leftKnee.Position, leftAnkle.Position),
                         Orientation = ToVec4(leftFootOrientation),
                        Valid = true
                    }
                )
                .AddChild(new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.FOOT_LEFT,
                    Point = AbsoluteToRelative(leftAnkle.Position, leftFoot.Position),
                     Orientation = ToVec4(leftFootOrientation)
                });

            // right leg
            var rightLeg = new OrientedJoint<Vector4, Vector3>
            {
                JointType = JointType.HIP_RIGHT,
                Point = AbsoluteToRelative(spine.Position, rightHip.Position),
                Orientation = ToVec4(rightKneeOrientation),
                Valid = true
            };
            rightLeg.Append(
                new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.KNEE_RIGHT,
                    Point = AbsoluteToRelative(rightHip.Position, rightKnee.Position),
                    Orientation = ToVec4(rightAnkleOrientation),
                    Valid = true
                }
                ).Append(
                    new OrientedJoint<Vector4, Vector3>
                    {
                        JointType = JointType.ANKLE_RIGHT,
                        Point = AbsoluteToRelative(rightKnee.Position, rightAnkle.Position),
                        Orientation = ToVec4(rightFootOrientation),
                        Valid = true
                    }
                )
                .AddChild(new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.FOOT_RIGHT,
                    Point = AbsoluteToRelative(rightAnkle.Position, rightFoot.Position),
                    Orientation = ToVec4(rightFootOrientation),
                });


            var jsNeck = Creator.CreateParent(new List<IJoint<Vector4, Vector3>>
            {
                leftArm,
                rightArm,
                new OrientedJoint<Vector4, Vector3>
                {
                    JointType = JointType.HEAD,
                    Point = AbsoluteToRelative(neck.Position, head.Position),
                    Orientation = ToVec4(headOrientation),
                    Valid = true
                }
            });
            jsNeck.JointType = JointType.NECK;
            jsNeck.Point = AbsoluteToRelative(spine.Position, neck.Position);
            jsNeck.Valid = true;
            jsNeck.Orientation = ToVec4(neckOrientation);
            s.UpdateSkeleton(JointType.NECK, jsNeck);
            s.UpdateSkeleton(JointType.HIP_LEFT, leftLeg);
            s.UpdateSkeleton(JointType.HIP_RIGHT, rightLeg);
            s.Valid = initSkeleton.TrackingState == SkeletonTrackingState.Tracked;
            s.Root.Orientation = ToVec4(spineOrientation);
            s.Root.Point = ToVec3(spine.Position) * 1000;
            return s;
        }

        private static Vector3 AbsoluteToRelative(SkeletonPoint parent, SkeletonPoint child)
        {
            return new Vector3(
                child.X - parent.X,
                child.Y - parent.Y,
                child.Z - parent.Z
                )*1000;
        }


        private static Vector4 ToVec4(Microsoft.Kinect.Vector4 v)
        {
            return new Vector4(v.X, v.Y, v.Z, v.W);
        }

        private static Vector3 ToVec3(Microsoft.Kinect.SkeletonPoint v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        private void FireNewSkeleton(ISkeleton<Vector4, Vector3> s)
        {
            if (NewSkeleton != null)
            {
                NewSkeleton(s);
            }
        }
    }
}