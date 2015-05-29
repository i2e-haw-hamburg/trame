using System;
using System.Threading;
using Leap;
using Trame.Implementation.Device.Adapter;
using Trame.Implementation.Skeleton;
using TrameSkeleton.Math;

namespace Trame.Implementation.Device
{
	/// <summary>
	/// Leap motion.
	/// </summary>
    class LeapMotion : IDevice
    {
        readonly LeapAdapter adapter = new LeapAdapter();
        private Thread t;
        private bool running = true;
        private ISkeleton lastSkeleton;
		/// <summary>
		/// Initializes a new instance of the <see cref="Trame.Implementation.Device.LeapMotion"/> class.
		/// </summary>
        public LeapMotion()
        {
            lastSkeleton = Creator.GetNewDefaultSkeleton();
            adapter.StartController(OnFrameArrived);
            t = new Thread(Run);
            t.Start();
        }

        private void Run()
        {
            while (running)
            {
                Thread.Sleep(200);
            }
        }

        public ISkeleton GetSkeleton()
        {
            return GetSkeleton(Creator.GetNewDefaultSkeleton());
        }

        public ISkeleton GetSkeleton(ISkeleton s)
        {
            var result = lastSkeleton;
            lastSkeleton = s;
            return result;
        }

        public void Stop()
        {
            adapter.Stop();
            running = false;
            t.Join();
        }
		/// <summary>
		/// Raises the frame arrived event.
		/// </summary>
		/// <param name="frame">Frame.</param>
        private void OnFrameArrived(Frame frame)
        {
            var leftWrist = lastSkeleton.GetJoint(JointType.WRIST_LEFT);
            var rightWrist = lastSkeleton.GetJoint(JointType.WRIST_RIGHT);

            var leftHand = LeftHand(leftWrist, frame);
            leftWrist.Valid = leftHand.Valid;
            leftWrist.Update(JointType.HAND_LEFT, leftHand);

            var rightHand = RightHand(rightWrist, frame);
            rightWrist.Valid = rightHand.Valid;
            rightWrist.Update(JointType.HAND_RIGHT, rightHand);

            lastSkeleton.UpdateSkeleton(JointType.WRIST_LEFT, leftWrist);
            lastSkeleton.UpdateSkeleton(JointType.WRIST_RIGHT, rightWrist);

            lastSkeleton.Valid = leftWrist.Valid && rightHand.Valid;

            FireNewSkeleton();
        }

        private void FireNewSkeleton()
        {
            if (NewSkeleton != null)
            {
                NewSkeleton(GetSkeleton());
            }
        }
		/// <summary>
		/// Lefts the hand.
		/// </summary>
		/// <returns>The hand.</returns>
		/// <param name="wrist">Wrist.</param>
		/// <param name="frame">Frame.</param>
        private static IJoint LeftHand(IJoint wrist, Frame frame)
        {
            var leftmost = new Hand();
            var hands = frame.Hands;
            if (hands.Leftmost.IsLeft)
            {
                leftmost = hands.Leftmost;
            }

            var left = BuildHand(leftmost, wrist, 1);
            left.JointType = JointType.HAND_LEFT;

            return left;
        }
		/// <summary>
		/// Rights the hand.
		/// </summary>
		/// <returns>The hand.</returns>
		/// <param name="wrist">Wrist.</param>
		/// <param name="frame">Frame.</param>
        private static IJoint RightHand(IJoint wrist, Frame frame)
        {
            var rightmost = new Hand();
            var hands = frame.Hands;
            if (hands.Rightmost.IsRight)
            {
                rightmost = hands.Rightmost;
            }

            var right = BuildHand(rightmost, wrist, 2);
            right.JointType = JointType.HAND_RIGHT;

            return right;
        }
		/// <summary>
		/// Builds the hand.
		/// </summary>
		/// <returns>The hand.</returns>
		/// <param name="hand">Hand.</param>
		/// <param name="wrist">Wrist.</param>
		/// <param name="side">Side.</param>
        private static IJoint BuildHand(Hand hand, IJoint wrist, int side)
        {
            var handJoint = new OrientedJoint();

            if (hand.IsValid)
            {
                var palmNormal = hand.PalmNormal;
                var handPosition = hand.PalmPosition;

                handJoint.Orientation = new Vector4(10 * palmNormal.x, 10 * palmNormal.y, 10 * palmNormal.z, 0);
                handJoint.Point = new Vector3(0, 0, -100);

                var fingers = hand.Fingers;
                foreach (var t in fingers)
                {
                    var normal = t.Direction;
                    var position = t.TipPosition - handPosition;

                    handJoint.AddChild(CreateFinger(position, normal, FingerType2JointType(t.Type(), side)));
                }
                handJoint.Valid = true;
            }

            return handJoint;
        }
		/// <summary>
		/// Creates the finger.
		/// </summary>
		/// <returns>The finger.</returns>
		/// <param name="position">Position.</param>
		/// <param name="normal">Normal.</param>
		/// <param name="jt">Jt.</param>
        private static IJoint CreateFinger(Vector position, Vector normal, JointType jt)
        {
            var finger = new OrientedJoint
            {
                JointType = jt,
                Point = new Vector3(position.x, position.y, position.z),
                Orientation = new Vector4(10*normal.x, 10*normal.y, 10*normal.z, 0)
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
    }
}
