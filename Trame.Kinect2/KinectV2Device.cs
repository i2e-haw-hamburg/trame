using System;
using System.Collections.Generic;
using System.Linq;
using Trame.Implementation;
using Trame.Interface;
using Trame.Math;

namespace Trame.Kinect2
{

    public class KinectV2Device : IDevice
    {
        private Microsoft.Kinect.KinectSensor _kinectSensor;
        private Microsoft.Kinect.CoordinateMapper _coordinateMapper;
        private Microsoft.Kinect.BodyFrameReader _bodyFrameReader;
        private int _displayWidth;
        private int _displayHeight;
        private IList<ISkeleton> _knownSkeletons;
        private Microsoft.Kinect.Body[] _bodyDataBuffer;

        private IDictionary<JointType, Microsoft.Kinect.JointType> mapping = new Dictionary<JointType, Microsoft.Kinect.JointType>
        {
            {JointType.NECK, Microsoft.Kinect.JointType.SpineShoulder},
            {JointType.CENTER, Microsoft.Kinect.JointType.SpineMid},
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

        private ISkeleton _lastAddedSkeleton;

        public KinectV2Device()
        {
            // one sensor is currently supported
            _kinectSensor = Microsoft.Kinect.KinectSensor.GetDefault();

            // get the coordinate mapper
            _coordinateMapper = _kinectSensor.CoordinateMapper;
            
            _knownSkeletons = new List<ISkeleton>();
        }

        private void BodyFrameReaderOnFrameArrived(object sender, Microsoft.Kinect.BodyFrameArrivedEventArgs bodyFrameArrivedEventArgs)
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
                foreach (Microsoft.Kinect.Body body in bodies)
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

        private ISkeleton CreateSkeleton(Microsoft.Kinect.Body body)
        {
            var newSkeleton = new InMapSkeleton { ID = (uint) body.TrackingId };

            UpdateSkeleton(body, newSkeleton);

            return newSkeleton;
        }

        private void UpdateSkeleton(Microsoft.Kinect.Body body, ISkeleton newSkeleton)
        {
            foreach (KeyValuePair<JointType, Microsoft.Kinect.JointType> jointMapping in mapping)
            {
                var joint = new OrientedJoint
                {
                    JointType = jointMapping.Key,
                    Point = ToVec3(body.Joints[jointMapping.Value].Position),
                    Orientation = ToVec4(body.JointOrientations[jointMapping.Value].Orientation),
                    Valid = body.Joints[jointMapping.Value].TrackingState == Microsoft.Kinect.TrackingState.Tracked
                };
                newSkeleton.UpdateSkeleton(joint.JointType, joint);
            }
        }

        /// <summary>
        /// Tries to read the new frame and returns wether actual data was read.
        /// </summary>
        /// <param name="bodyFrameArrivedEventArgs"></param>
        /// <returns></returns>
        private bool ReadBodyFrame(Microsoft.Kinect.BodyFrameArrivedEventArgs bodyFrameArrivedEventArgs)
        {
            using (Microsoft.Kinect.BodyFrame bodyFrame = bodyFrameArrivedEventArgs.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (_bodyDataBuffer == null || _bodyDataBuffer.Length != bodyFrame.BodyCount)
                    {
                        _bodyDataBuffer = new Microsoft.Kinect.Body[bodyFrame.BodyCount];
                    }
                    
                    bodyFrame.GetAndRefreshBodyData(_bodyDataBuffer);
                    return true;
                }
            }

            return false;
        }

        private void KinectSensorOnIsAvailableChanged(object sender, Microsoft.Kinect.IsAvailableChangedEventArgs isAvailableChangedEventArgs)
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
        private static Vector3 ToVec3(Microsoft.Kinect.CameraSpacePoint point)
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
    }
}
