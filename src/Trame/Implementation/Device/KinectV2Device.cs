extern alias KinectV2;
using KinectV2::Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Linq;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Math;
using Vector4 = TrameSkeleton.Math.Vector4;

namespace Trame.Implementation.Device
{
    extern alias KinectV1;

    internal class KinectV2Device : IDevice
    {
        private KinectV2::Microsoft.Kinect.KinectSensor _kinectSensor;
        private KinectV2::Microsoft.Kinect.CoordinateMapper _coordinateMapper;
        private KinectV2::Microsoft.Kinect.BodyFrameReader _bodyFrameReader;
        private int _displayWidth;
        private int _displayHeight;
        private IList<ISkeleton> _knownSkeletons;
        private KinectV2::Microsoft.Kinect.Body[] _bodyDataBuffer;

        private IDictionary<JointType, KinectV2::Microsoft.Kinect.JointType> mapping = new Dictionary<JointType, KinectV2::Microsoft.Kinect.JointType>
        {
            {JointType.NECK, KinectV2::Microsoft.Kinect.JointType.SpineShoulder},
            {JointType.CENTER, KinectV2::Microsoft.Kinect.JointType.SpineMid},
            {JointType.HEAD, KinectV2::Microsoft.Kinect.JointType.Head},
            {JointType.SHOULDER_LEFT, KinectV2::Microsoft.Kinect.JointType.ShoulderLeft},
            {JointType.SHOULDER_RIGHT, KinectV2::Microsoft.Kinect.JointType.ShoulderRight},
            {JointType.ELBOW_LEFT, KinectV2::Microsoft.Kinect.JointType.ElbowLeft},
            {JointType.ELBOW_RIGHT, KinectV2::Microsoft.Kinect.JointType.ElbowRight},
            {JointType.WRIST_LEFT, KinectV2::Microsoft.Kinect.JointType.WristLeft},
            {JointType.WRIST_RIGHT, KinectV2::Microsoft.Kinect.JointType.WristRight},
            {JointType.HAND_LEFT, KinectV2::Microsoft.Kinect.JointType.HandLeft},
            {JointType.HAND_RIGHT, KinectV2::Microsoft.Kinect.JointType.HandRight},
            {JointType.HIP_LEFT, KinectV2::Microsoft.Kinect.JointType.HipLeft},
            {JointType.HIP_RIGHT, KinectV2::Microsoft.Kinect.JointType.HipRight},
            {JointType.KNEE_LEFT, KinectV2::Microsoft.Kinect.JointType.KneeLeft},
            {JointType.KNEE_RIGHT, KinectV2::Microsoft.Kinect.JointType.KneeRight},
            {JointType.ANKLE_LEFT, KinectV2::Microsoft.Kinect.JointType.AnkleLeft},
            {JointType.ANKLE_RIGHT, KinectV2::Microsoft.Kinect.JointType.AnkleRight},
            {JointType.FOOT_LEFT, KinectV2::Microsoft.Kinect.JointType.FootLeft},
            {JointType.FOOT_RIGHT, KinectV2::Microsoft.Kinect.JointType.FootRight},
        };

        private ISkeleton _lastAddedSkeleton;

        public KinectV2Device()
        {
            // one sensor is currently supported
            _kinectSensor = KinectV2::Microsoft.Kinect.KinectSensor.GetDefault();

            // get the coordinate mapper
            _coordinateMapper = _kinectSensor.CoordinateMapper;
            
            _knownSkeletons = new List<ISkeleton>();
        }

        private void BodyFrameReaderOnFrameArrived(object sender, KinectV2::Microsoft.Kinect.BodyFrameArrivedEventArgs bodyFrameArrivedEventArgs)
        {
            if (ReadBodyFrame(bodyFrameArrivedEventArgs))
            {
                UpdateSkeletons();
            }
        }

        private void UpdateSkeletons()
        {
            if (_bodyDataBuffer.Length > 0)
            {
                var bodies = _bodyDataBuffer.Where(body => body.IsTracked);
                foreach (KinectV2::Microsoft.Kinect.Body body in bodies)
                {
                    var knownSkeleton = _knownSkeletons.FirstOrDefault(skeleton => skeleton.ID == (uint)body.TrackingId);

                    if (knownSkeleton != null)
                    {
                        UpdateSkeleton(body, knownSkeleton);
                    }
                    else
                    {
                        _lastAddedSkeleton = CreateSkeleton(body);
                        _knownSkeletons.Add(_lastAddedSkeleton);
                        NewSkeleton?.Invoke(_lastAddedSkeleton);
                    }
                }
            }
        }

        private ISkeleton CreateSkeleton(KinectV2::Microsoft.Kinect.Body body)
        {
            var newSkeleton = new InMapSkeleton { ID = (uint) body.TrackingId };

            UpdateSkeleton(body, newSkeleton);

            return newSkeleton;
        }

        private void UpdateSkeleton(KinectV2::Microsoft.Kinect.Body body, ISkeleton newSkeleton)
        {
            foreach (KeyValuePair<JointType, KinectV2::Microsoft.Kinect.JointType> jointMapping in mapping)
            {
                var joint = new OrientedJoint
                {
                    JointType = jointMapping.Key,
                    Point = ToVec3(body.Joints[jointMapping.Value].Position),
                    Orientation = ToVec4(body.JointOrientations[jointMapping.Value].Orientation),
                    Valid = body.Joints[jointMapping.Value].TrackingState == KinectV2::Microsoft.Kinect.TrackingState.Tracked
                };
                newSkeleton.UpdateSkeleton(joint.JointType, joint);
            }
        }

        /// <summary>
        /// Tries to read the new frame and returns wether actual data was read.
        /// </summary>
        /// <param name="bodyFrameArrivedEventArgs"></param>
        /// <returns></returns>
        private bool ReadBodyFrame(KinectV2::Microsoft.Kinect.BodyFrameArrivedEventArgs bodyFrameArrivedEventArgs)
        {
            using (KinectV2::Microsoft.Kinect.BodyFrame bodyFrame = bodyFrameArrivedEventArgs.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (_bodyDataBuffer == null || _bodyDataBuffer.Length != bodyFrame.BodyCount)
                    {
                        _bodyDataBuffer = new KinectV2::Microsoft.Kinect.Body[bodyFrame.BodyCount];
                    }
                    
                    bodyFrame.GetAndRefreshBodyData(_bodyDataBuffer);
                    return true;
                }
            }

            return false;
        }

        private void KinectSensorOnIsAvailableChanged(object sender, KinectV2::Microsoft.Kinect.IsAvailableChangedEventArgs isAvailableChangedEventArgs)
        {
            Console.WriteLine("Kinect availability status changed to {0}." , isAvailableChangedEventArgs.IsAvailable);
        }

        public ISkeleton GetSkeleton()
        {
            return _lastAddedSkeleton;
        }

        public ISkeleton GetSkeleton(ISkeleton baseSkeleton)
        {
            return _lastAddedSkeleton;
        }

        public void Start()
        {
            Console.WriteLine("Device starting");
            _kinectSensor.Open();
            _kinectSensor.IsAvailableChanged += KinectSensorOnIsAvailableChanged;

            // open the reader for the body frames
            _bodyFrameReader = _kinectSensor.BodyFrameSource.OpenReader();
            _bodyFrameReader.FrameArrived += BodyFrameReaderOnFrameArrived;
        }

        public void Stop()
        {
            Console.WriteLine("Device stopping");
            _kinectSensor.Close();
            _kinectSensor.IsAvailableChanged -= KinectSensorOnIsAvailableChanged;
            _bodyFrameReader.FrameArrived -= BodyFrameReaderOnFrameArrived;
        }

        public event Action<ISkeleton> NewSkeleton;

        /// <summary>
        /// Absolutes to relative.
        /// </summary>
        /// <returns>The to relative.</returns>
        /// <param name="point">point.</param>
        private static Vector3 ToVec3(KinectV2::Microsoft.Kinect.CameraSpacePoint point)
        {
            return new Vector3(point.X, point.Y, point.Z) * 1000;
        }

        /// <summary>
        /// Tos the vec4.
        /// </summary>
        /// <returns>The vec4.</returns>
        /// <param name="v">V.</param>
        private static Vector4 ToVec4(KinectV2::Microsoft.Kinect.Vector4 v)
        {
            return new Vector4(v.X, v.Y, v.Z, v.W);
        }
    }
}
