using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Kinect;
using Trame.Implementation;
using Trame.Interface;
using Trame.Math;
using JointType = Microsoft.Kinect.JointType;
using Timer = System.Timers.Timer;
using Vector4 = Trame.Math.Vector4;

namespace Trame.Kinect
{
	/// <summary>
	/// Kinect device.
	/// </summary>
    public class KinectDevice : IDevice
    {
        private readonly KinectAdapter _adapter = new KinectAdapter();
        private ISkeleton _lastSkeleton;
        
        private IDictionary<Interface.JointType, JointType> mapping = new Dictionary<Interface.JointType, JointType>
        {
            {Interface.JointType.NECK, JointType.ShoulderCenter},
            {Interface.JointType.CENTER, JointType.Spine},
            {Interface.JointType.HEAD, JointType.Head},
            {Interface.JointType.SHOULDER_LEFT, JointType.ShoulderLeft},
            {Interface.JointType.SHOULDER_RIGHT, JointType.ShoulderRight},
            {Interface.JointType.ELBOW_LEFT, JointType.ElbowLeft},
            {Interface.JointType.ELBOW_RIGHT, JointType.ElbowRight},
            {Interface.JointType.WRIST_LEFT, JointType.WristLeft},
            {Interface.JointType.WRIST_RIGHT, JointType.WristRight},
            {Interface.JointType.HAND_LEFT, JointType.HandLeft},
            {Interface.JointType.HAND_RIGHT, JointType.HandRight},
            {Interface.JointType.HIP_LEFT, JointType.HipLeft},
            {Interface.JointType.HIP_RIGHT, JointType.HipRight},
            {Interface.JointType.KNEE_LEFT, JointType.KneeLeft},
            {Interface.JointType.KNEE_RIGHT, JointType.KneeRight},
            {Interface.JointType.ANKLE_LEFT, JointType.AnkleLeft},
            {Interface.JointType.ANKLE_RIGHT, JointType.AnkleRight},
            {Interface.JointType.FOOT_LEFT, JointType.FootLeft},
            {Interface.JointType.FOOT_RIGHT, JointType.FootRight},
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
                var foundedSkeletons = new Skeleton[frame.SkeletonArrayLength];

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
	    private ISkeleton CreateSkeleton(Skeleton initSkeleton)
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