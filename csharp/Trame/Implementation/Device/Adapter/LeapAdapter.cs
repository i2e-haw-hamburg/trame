using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge.Math;
using Leap;
using Trame.Implementation.Skeleton;

namespace Trame.Implementation.Device.Adapter
{
    class LeapAdapter
    {
        readonly Controller leapController = new Controller();

        public IJoint LeftHand(IJoint wrist)
        {
            var leftmost = new Hand();

            if (leapController.IsConnected)
            {
                Frame frame = leapController.Frame();
                HandList hands = frame.Hands;
                if (hands.Leftmost.IsLeft)
                {
                    leftmost = hands.Leftmost;
                }
            }

            IJoint left = BuildHand(leftmost, wrist, 1);
            left.JointType = JointType.HAND_LEFT;

            return left;
        }

        public IJoint RightHand(IJoint wrist)
        {
            var rightmost = new Hand();

            if (leapController.IsConnected)
            {
                Frame frame = leapController.Frame();
                HandList hands = frame.Hands;
                if (hands.Rightmost.IsRight)
                {
                    rightmost = hands.Rightmost;
                }
            }

            IJoint right = BuildHand(rightmost, wrist, 2);
            right.JointType = JointType.HAND_RIGHT;

            return right;
        }

        private static IJoint BuildHand(Hand hand, IJoint wrist, int side)
        {
            IJoint handJoint = new Joint();

            if (hand.IsValid)
            {
                Vector palmNormal = hand.PalmNormal;
                Vector handPosition = hand.PalmPosition;

                handJoint.Normal = new Vector3(100*palmNormal.x, 100*palmNormal.y, 100*palmNormal.z);
                handJoint.Point = new Vector3(0, 0, -100);

                FingerList fingers = hand.Fingers;
                foreach (var t in fingers)
                {
                    Vector normal = t.Direction;
                    Vector position = t.TipPosition - handPosition;

                    handJoint.AddChild(CreateFinger(position, normal, FingerType2JointType(t.Type(), side)));
                }
                handJoint.Valid = true;
            }

            return handJoint;
        }

        private static IJoint CreateFinger(Vector position, Vector normal, JointType jt)
        {
            var finger = new Joint();
            finger.JointType = jt;
            finger.Point = new Vector3(position.x, position.y, position.z);
            finger.Normal = new Vector3(100 * normal.x, 100 * normal.y, 100 * normal.z);
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
    }
}
