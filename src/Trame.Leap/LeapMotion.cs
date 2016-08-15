using System;
using System.Collections.Generic;
using System.Linq;
using Leap;
using Trame.Implementation;
using Trame.Interface;
using Trame.Math;

namespace Trame.Leap
{
	/// <summary>
	/// Leap motion.
	/// </summary>
    public class LeapMotion : IDevice
    {
        readonly LeapAdapter _adapter = new LeapAdapter();
        private ISkeleton _lastSkeleton;
		/// <summary>
		/// Initializes a new instance of the <see cref="LeapMotion"/> class.
		/// </summary>
        public LeapMotion()
        {
            _lastSkeleton = Creator.GetNewDefaultSkeleton<InMapSkeleton>();
        }

        public ISkeleton GetSkeleton()
        {
            return GetSkeleton(Creator.GetNewDefaultSkeleton<InMapSkeleton>());
        }

        public ISkeleton GetSkeleton(ISkeleton s)
        {
            var result = _lastSkeleton;
            _lastSkeleton = s;
            return result;
        }

        public void Stop()
        {
            _adapter.Stop();
        }

	    public void Start()
	    {
            _adapter.StartController(OnFrameArrived);
        }

	    /// <summary>
		/// Raises the frame arrived event.
		/// </summary>
		/// <param name="frame">Frame.</param>
        private void OnFrameArrived(Frame frame)
        {
            var leftHand = LeftHand(frame);
            var rightHand = RightHand(frame);
            
            var leftWrist = _lastSkeleton.GetJoint(JointType.WRIST_LEFT);
		    foreach (var joint in leftHand)
		    {
		        joint.Point += leftWrist.Point;
		        _lastSkeleton.UpdateSkeleton(joint.JointType, joint);
		    }

            var rightWrist = _lastSkeleton.GetJoint(JointType.WRIST_RIGHT);
            foreach (var joint in rightHand)
            {
                joint.Point += rightWrist.Point;
                _lastSkeleton.UpdateSkeleton(joint.JointType, joint);
            }
            FireNewSkeleton();
        }

        private void FireNewSkeleton()
        {
            NewSkeleton?.Invoke(GetSkeleton());
        }

	    /// <summary>
	    /// Lefts the hand.
	    /// </summary>
	    /// <returns>The hand.</returns>
	    /// <param name="frame">Frame.</param>
        private static IEnumerable<IJoint> LeftHand(Frame frame)
        {
            var leftmost = new Hand();
            var hands = frame.Hands;
            if (hands.Leftmost.IsLeft)
            {
                leftmost = hands.Leftmost;
            }

            var leftHand = BuildHand(leftmost, 1);
            leftHand[0].JointType = JointType.HAND_LEFT;

            return leftHand;
        }

	    /// <summary>
	    /// Rights the hand.
	    /// </summary>
	    /// <returns>The hand.</returns>
	    /// <param name="frame">Frame.</param>
        private static IEnumerable<IJoint> RightHand(Frame frame)
        {
            var rightmost = new Hand();
            var hands = frame.Hands;
            if (hands.Rightmost.IsRight)
            {
                rightmost = hands.Rightmost;
            }

            var rightHand = BuildHand(rightmost, 2);
            rightHand[0].JointType = JointType.HAND_RIGHT;

            return rightHand;
        }

	    /// <summary>
	    /// Builds the hand.
	    /// </summary>
	    /// <returns>The hand.</returns>
	    /// <param name="hand">Hand.</param>
	    /// <param name="side">Side.</param>
	    private static IList<IJoint> BuildHand(Hand hand, int side)
        {
            var handJoint = new OrientedJoint();

		    if (!hand.IsValid)
		    {
		        return new IJoint[]{handJoint};
		    }
		    var palmNormal = hand.PalmNormal;
		    var handPosition = hand.PalmPosition;

		    handJoint.Orientation = new Vector4(palmNormal.x, palmNormal.y, palmNormal.z, 0);
		    handJoint.Point = new Vector3(0, 0, -100);
            var joints = new List<IJoint> { handJoint };
		    var fingers = hand.Fingers;
	        joints.AddRange(from t in fingers let normal = t.Direction let position = t.TipPosition - handPosition select CreateFinger(position, normal, FingerType2JointType(t.Type(), side)));
	        handJoint.Valid = true;

            return joints;
        }
		/// <summary>
		/// Creates the finger.
		/// </summary>
		/// <returns>The finger.</returns>
		/// <param name="position">Position.</param>
		/// <param name="normal">Normal.</param>
		/// <param name="jt">Joint type</param>
        private static IJoint CreateFinger(Vector position, Vector normal, JointType jt)
        {
            var finger = new OrientedJoint
            {
                JointType = jt,
                Point = new Vector3(position.x, position.y, position.z),
                Orientation = new Vector4(normal.x, normal.y, normal.z, 0)
            };
            return finger;
        }

        private static JointType FingerType2JointType(Finger.FingerType ft, int side)
        {
            JointType jt;

            switch (ft)
            {
                case Finger.FingerType.TYPE_INDEX:
                    jt = (side == 1) ? JointType.INDEX_FINGER_LEFT : JointType.INDEX_FINGER_RIGHT;
                    break;
                case Finger.FingerType.TYPE_MIDDLE:
                    jt = (side == 1) ? JointType.MIDDLE_FINGER_LEFT : JointType.MIDDLE_FINGER_RIGHT;
                    break;
                case Finger.FingerType.TYPE_PINKY:
                    jt = (side == 1) ? JointType.LITTLE_FINGER_LEFT : JointType.LITTLE_FINGER_RIGHT;
                    break;
                case Finger.FingerType.TYPE_RING:
                    jt = (side == 1) ? JointType.RING_FINGER_LEFT : JointType.RING_FINGER_RIGHT;
                    break;
                case Finger.FingerType.TYPE_THUMB:
                    jt = (side == 1) ? JointType.THUMB_LEFT : JointType.THUMB_RIGHT;
                    break;
                default:
                    jt = JointType.UNSPECIFIED;
                    break;
            }

            return jt;
        }

        public event Action<ISkeleton> NewSkeleton;

	    public void SetSkeleton(ISkeleton skeleton)
	    {
	        _lastSkeleton = skeleton;
	    }
    }
}
