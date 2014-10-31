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
                leftmost = hands.Leftmost;
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
                rightmost = hands.Rightmost;
            }

            IJoint right = BuildHand(rightmost, wrist, 1);
            right.JointType = JointType.HAND_RIGHT;

            return right;
        }

        private static IJoint BuildHand(Hand hand, IJoint wrist, int side)
        {
            IJoint thumb = new Joint();
            IJoint index = new Joint();
            IJoint middle = new Joint();
            IJoint ring = new Joint();
            IJoint little = new Joint();
            IJoint handJoint = new Joint();

            if (side == 1)
            {
                thumb.JointType = JointType.THUMB_LEFT;
                index.JointType = JointType.INDEX_FINGER_LEFT;
                middle.JointType = JointType.MIDDLE_FINGER_LEFT;
                ring.JointType = JointType.RING_FINGER_LEFT;
                little.JointType = JointType.LITTLE_FINGER_LEFT;
            }
            else
            {
                thumb.JointType = JointType.THUMB_RIGHT;
                index.JointType = JointType.INDEX_FINGER_RIGHT;
                middle.JointType = JointType.MIDDLE_FINGER_RIGHT;
                ring.JointType = JointType.RING_FINGER_RIGHT;
                little.JointType = JointType.LITTLE_FINGER_RIGHT;
            }

            if (hand.IsValid)
            {
                Vector palmNormal = hand.PalmNormal;
                Vector handPosition = hand.PalmPosition;

                handJoint.Normal = new Vector3(100*palmNormal.x, 100*palmNormal.y, 100*palmNormal.z);
                handJoint.Point = new Vector3(0, 0, -100);

                FingerList fingers = hand.Fingers;
                if (fingers.Count > 0)
                {
                    Vector normal = fingers[0].Direction;
                    Vector position = fingers[0].TipPosition - handPosition;

                    thumb.Point = new Vector3(position.x, position.y, position.z);
                    thumb.Normal = new Vector3(100 * normal.x, 100 * normal.y, 100 * normal.z);
                    handJoint.AddChild(thumb);
                }

                handJoint.Valid = true;
            }

            return handJoint;
        }
    }
}
