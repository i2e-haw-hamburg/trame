using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Kinect;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Math;
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
        
        private IDictionary<JointType, Microsoft.Kinect.JointType> mapping = new Dictionary<JointType, Microsoft.Kinect.JointType>
        {
            {JointType.NECK, Microsoft.Kinect.JointType.ShoulderCenter},
            {JointType.CENTER, Microsoft.Kinect.JointType.Spine},
            {JointType.HEAD, Microsoft.Kinect.JointType.Head},
            {JointType.SHOULDER_LEFT, Microsoft.Kinect.JointType.ShoulderLeft},
            {JointType.SHOULDER_RIGHT, Microsoft.Kinect.JointType.ShoulderRight},
            {JointType.ELBOW_LEFT, Microsoft.Kinect.JointType.ElbowLeft},
            {JointType.ELBOW_RIGHT, Microsoft.Kinect.JointType.ElbowRight},
            {JointType.WRIST_LEFT, Microsoft.Kinect.JointType.WristLeft},
            {JointType.WRIST_RIGHT, Microsoft.Kinect.JointType.WristRight},
            {JointType.HAND_LEFT, Microsoft.Kinect.JointType.HandLeft},
            {JointType.HAND_RIGHT, Microsoft.Kinect.JointType.HandRight},
            {JointType.HIP_LEFT, Microsoft.Kinect.JointType.HipLeft},
            {JointType.HIP_RIGHT, Microsoft.Kinect.JointType.HipRight},
            {JointType.KNEE_LEFT, Microsoft.Kinect.JointType.KneeLeft},
            {JointType.KNEE_RIGHT, Microsoft.Kinect.JointType.KneeRight},
            {JointType.ANKLE_LEFT, Microsoft.Kinect.JointType.AnkleLeft},
            {JointType.ANKLE_RIGHT, Microsoft.Kinect.JointType.AnkleRight},
            {JointType.FOOT_LEFT, Microsoft.Kinect.JointType.FootLeft},
            {JointType.FOOT_RIGHT, Microsoft.Kinect.JointType.FootRight},
        };

		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Device.KinectDevice"/> class.
		/// </summary>
        public KinectDevice()
        {
            _adapter.StartKinect(OnFrameArrived);
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
                var foundedSkeletons = new Microsoft.Kinect.Skeleton[frame.SkeletonArrayLength];

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
	    private ISkeleton CreateSkeleton(Microsoft.Kinect.Skeleton initSkeleton)
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
        private static Vector4 ToVec4(Microsoft.Kinect.Vector4 v)
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