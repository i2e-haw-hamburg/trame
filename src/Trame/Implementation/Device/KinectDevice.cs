﻿extern alias KinectV1;

using System;
using System.Collections.Generic;
using System.Linq;
using KinectV1::Microsoft.Kinect;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Math;
using Timer = System.Timers.Timer;
using Vector4 = TrameSkeleton.Math.Vector4;

namespace Trame.Implementation.Device
{
	/// <summary>
	/// Kinect device.
	/// </summary>
    internal class KinectDevice : IDevice
    {
        private readonly KinectAdapter _adapter = new KinectAdapter();
        private ISkeleton _lastSkeleton;
        
        private IDictionary<JointType, KinectV1::Microsoft.Kinect.JointType> mapping = new Dictionary<JointType, KinectV1::Microsoft.Kinect.JointType>
        {
            {JointType.NECK, KinectV1::Microsoft.Kinect.JointType.ShoulderCenter},
            {JointType.CENTER, KinectV1::Microsoft.Kinect.JointType.Spine},
            {JointType.HEAD, KinectV1::Microsoft.Kinect.JointType.Head},
            {JointType.SHOULDER_LEFT, KinectV1::Microsoft.Kinect.JointType.ShoulderLeft},
            {JointType.SHOULDER_RIGHT, KinectV1::Microsoft.Kinect.JointType.ShoulderRight},
            {JointType.ELBOW_LEFT, KinectV1::Microsoft.Kinect.JointType.ElbowLeft},
            {JointType.ELBOW_RIGHT, KinectV1::Microsoft.Kinect.JointType.ElbowRight},
            {JointType.WRIST_LEFT, KinectV1::Microsoft.Kinect.JointType.WristLeft},
            {JointType.WRIST_RIGHT, KinectV1::Microsoft.Kinect.JointType.WristRight},
            {JointType.HAND_LEFT, KinectV1::Microsoft.Kinect.JointType.HandLeft},
            {JointType.HAND_RIGHT, KinectV1::Microsoft.Kinect.JointType.HandRight},
            {JointType.HIP_LEFT, KinectV1::Microsoft.Kinect.JointType.HipLeft},
            {JointType.HIP_RIGHT, KinectV1::Microsoft.Kinect.JointType.HipRight},
            {JointType.KNEE_LEFT, KinectV1::Microsoft.Kinect.JointType.KneeLeft},
            {JointType.KNEE_RIGHT, KinectV1::Microsoft.Kinect.JointType.KneeRight},
            {JointType.ANKLE_LEFT, KinectV1::Microsoft.Kinect.JointType.AnkleLeft},
            {JointType.ANKLE_RIGHT, KinectV1::Microsoft.Kinect.JointType.AnkleRight},
            {JointType.FOOT_LEFT, KinectV1::Microsoft.Kinect.JointType.FootLeft},
            {JointType.FOOT_RIGHT, KinectV1::Microsoft.Kinect.JointType.FootRight},
        };

		/// <summary>
		/// Initializes a new instanKinectDevice="Device.KinectDevice"/> class.
		/// </summary>
        public KinectDevice()
        {
            _lastSkeleton = Creator.GetNewDefaultSkeleton<InMapSkeleton>();
        }

        public ISkeleton GetSkeleton()
        {
            return _lastSkeleton;
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            return _lastSkeleton;
        }

        public void Stop()
        {
            _adapter.StopKinect();
        }

	    public void Start()
	    {
	        try
	        {
                _adapter.StartKinect(OnFrameArrived);
                return;
            }
	        catch (Exception)
	        {
	            try
	            {
                    Stop();
	            }
	            catch (Exception)
	            {}
	            var t = new Timer(100);
	            t.AutoReset = false;
	            t.Elapsed += (sender, args) => Start();
                t.Start();
	        }
        }

	    public event Action<ISkeleton> NewSkeleton;

		/// <summary>
		/// Raises the frame arrived event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
        private void OnFrameArrived(object sender, SkeletonFrameReadyEventArgs e)
        {
		    using (var frame = e.OpenSkeletonFrame())
            {
                if (frame == null)
                {
                    return;
                }
                var foundedSkeletons = new KinectV1::Microsoft.Kinect.Skeleton[frame.SkeletonArrayLength];

                frame.CopySkeletonDataTo(foundedSkeletons);

                var skeletons = foundedSkeletons.Where(s => s.TrackingState == SkeletonTrackingState.Tracked);
                foreach (var skeleton in skeletons)
                {
                    var trameSkeleton = CreateSkeleton(skeleton);
                    _lastSkeleton = trameSkeleton;
                    FireNewSkeleton(trameSkeleton);
                }
            }
        }
        
        /// <summary>
        /// Creates the skeleton.
        /// </summary>
        /// <returns>The skeleton.</returns>
        /// <param name="initSkeleton">Init skeleton.</param>
	    private ISkeleton CreateSkeleton(KinectV1::Microsoft.Kinect.Skeleton initSkeleton)
	    {
            var s = new InMapSkeleton { ID = (uint)initSkeleton.TrackingId };
	        foreach (var jointMapping in mapping)
	        {
                var joint = new OrientedJoint
                {
                    JointType = jointMapping.Key,
                    Point = ToVec3(initSkeleton.Joints[jointMapping.Value].Position),
                    Orientation = ToVec4(initSkeleton.BoneOrientations[jointMapping.Value].AbsoluteRotation.Quaternion),
                    Valid = initSkeleton.Joints[jointMapping.Value].TrackingState == JointTrackingState.Tracked
                };
                s.UpdateSkeleton(joint.JointType, joint);

            }
            // TODO: iterate over all data in array 
            s.Valid = initSkeleton.TrackingState == SkeletonTrackingState.Tracked;
	        return s;
	    }

		/// <summary>
		/// Absolutes to relative.
		/// </summary>
		/// <returns>The to relative.</returns>
        /// <param name="point">point.</param>
        private static Vector3 ToVec3(SkeletonPoint point)
        {
            return new Vector3(point.X, point.Y, point.Z) * 1000;
        }

		/// <summary>
		/// Tos the vec4.
		/// </summary>
		/// <returns>The vec4.</returns>
		/// <param name="v">V.</param>
        private static Vector4 ToVec4(KinectV1::Microsoft.Kinect.Vector4 v)
        {
            return new Vector4(v.X, v.Y, v.Z, v.W);
        }

		/// <summary>
		/// Fires the new skeleton.
		/// </summary>
		/// <param name="s">S.</param>
        private void FireNewSkeleton(ISkeleton s)
        {
            if (NewSkeleton != null)
            {
                NewSkeleton(s);
            }
        }
    }
}