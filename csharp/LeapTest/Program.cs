using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AForge.Math;
using Leap;
using Trame;
using Trame.Implementation.Skeleton;

namespace LeapTest
{
    class Program
    {

        static ISkeleton lastSkeleton = Creator.GetNewDefaultSkeleton();

        static void Init()
        {
            var count = 0;
            var start = System.DateTime.Now;
            var con = new Controller(new FrameListener(x =>
            {
                count++;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Frames: " + count);
                var cur = System.DateTime.Now - start;
                Console.Write(" | FPS: " + (count / cur.TotalSeconds));
                OnFrameArrived(x);
            }));
            con.SetPolicy(Controller.PolicyFlag.POLICY_BACKGROUND_FRAMES);
        }

        private static void OnFrameArrived(Frame frame)
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
        }


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

        private static IJoint BuildHand(Hand hand, IJoint wrist, int side)
        {
            var handJoint = new Joint();

            if (hand.IsValid)
            {
                var palmNormal = hand.PalmNormal;
                var handPosition = hand.PalmPosition;

                handJoint.Normal = new Vector3(10 * palmNormal.x, 10 * palmNormal.y, 10 * palmNormal.z);
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

        private static IJoint CreateFinger(Vector position, Vector normal, JointType jt)
        {
            var finger = new Joint();
            finger.JointType = jt;
            finger.Point = new Vector3(position.x, position.y, position.z);
            finger.Normal = new Vector3(10 * normal.x, 10 * normal.y, 10 * normal.z);
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

        static void Main(string[] args)
        {
            Init();
            Console.ReadKey();
        }
    }

    class FrameListener : Listener
    {
        private Action<Frame> onFrameArrived;

        public FrameListener(Action<Frame> onFrameArrived)
        {
            this.onFrameArrived = onFrameArrived;
        }

        public override void OnFrame(Controller controller)
        {
            onFrameArrived(controller.Frame());
        }
    }
}
