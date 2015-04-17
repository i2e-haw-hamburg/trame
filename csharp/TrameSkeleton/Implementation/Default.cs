using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AForge.Math;

namespace Trame.Implementation.Skeleton
{
    class Default
    {
        public static ISkeleton<Vector4, Vector3> CreateSkeleton()
        {
            int centerY = 1100;
            int upperBodyY = 350;
            var centerOrientation = new Vector4(0, 0, 0, 0);
            var s = new Skeleton<Vector4, Vector3> { Valid = true };

            var leftShoulder = CreateArm(Side.LEFT);
            var rightShoulder = CreateArm(Side.RIGHT);
            var leftHip = CreateLeg(Side.LEFT);
            var rightHip = CreateLeg(Side.RIGHT);
            var head = CreateHead();

            IJoint<Vector4, Vector3> neck = Creator.CreateParent(new List<IJoint<Vector4, Vector3>> { head, rightShoulder, leftShoulder });
            neck.Point = new Vector3(0, upperBodyY, 0);
            neck.JointType = JointType.NECK;
            neck.Valid = true;

            IJoint<Vector4, Vector3> center = Creator.CreateParent(new List<IJoint<Vector4, Vector3>> { neck, rightHip, leftHip });
            center.Orientation = centerOrientation;
            center.Point = new Vector3(0, centerY, 0);
            center.JointType = JointType.CENTER;
            center.Valid = true;

            s.Root = center;

            return s;
        }

        public static IJoint<Vector4, Vector3> CreateArm(Side side)
        {
            int handLength = 50;
            int forearmX = 50;
            int forearmY = 355;
            int armX = 75;
            int armY = 320;
            int shoulderX = 220;
            var handOrientation = new Vector4(100, 0, 0, 0);

            var shoulder = new OrientedJoint<Vector4, Vector3>();
            var elbow = new OrientedJoint<Vector4, Vector3>();
            var wrist = new OrientedJoint<Vector4, Vector3>();
            var hand = new OrientedJoint<Vector4, Vector3>();

            if (side == Side.LEFT)
            {
                shoulder.JointType = JointType.SHOULDER_LEFT;
                elbow.JointType = JointType.ELBOW_LEFT;
                wrist.JointType = JointType.WRIST_LEFT;
                hand.JointType = JointType.HAND_LEFT;
            }
            else
            {
                shoulder.JointType = JointType.SHOULDER_RIGHT;
                elbow.JointType = JointType.ELBOW_RIGHT;
                wrist.JointType = JointType.WRIST_RIGHT;
                hand.JointType = JointType.HAND_RIGHT;
            }

            int s = Convert.ToInt32(side);

            hand.Orientation = handOrientation * -s;
            hand.Point = new Vector3(0, -handLength, 0);
            hand.Valid = true;

            wrist.Point = new Vector3(s * forearmX, -forearmY, 0);
            wrist.AddChild(hand);
            wrist.Valid = true;

            // elbows relative to shoulders
            elbow.Point = new Vector3(s * armX, -armY, 0);
            elbow.Valid = true;
            elbow.AddChild(wrist);

            // shoulders relative to neck
            shoulder.Point = new Vector3(s * shoulderX, 0, 0);
            shoulder.Valid = true;
            shoulder.AddChild(elbow);

            return shoulder;
        }

        public static IJoint<Vector4, Vector3> CreateLeg(Side side)
        {
            int footLength = 255;
            int lowerLegY = 410;
            int thighY = 540;
            int hipX = 180;
            int hipY = 100;

            var footOrientation = new Vector4(0, 0, -100, 0);

            var foot = new OrientedJoint<Vector4, Vector3>();
            var ankle = new OrientedJoint<Vector4, Vector3>();
            var knee = new OrientedJoint<Vector4, Vector3>();
            var hip = new OrientedJoint<Vector4, Vector3>();

            if (side == Side.LEFT)
            {
                foot.JointType = JointType.FOOT_LEFT;
                ankle.JointType = JointType.ANKLE_LEFT;
                knee.JointType = JointType.KNEE_LEFT;
                hip.JointType = JointType.HIP_LEFT;
            }
            else
            {
                foot.JointType = JointType.FOOT_RIGHT;
                ankle.JointType = JointType.ANKLE_RIGHT;
                knee.JointType = JointType.KNEE_RIGHT;
                hip.JointType = JointType.HIP_RIGHT;
            }

            int s = Convert.ToInt32(side);

            foot.Orientation = footOrientation;
            foot.Point = new Vector3(0, 0, -footLength);
            foot.Valid = true;

            ankle.Point = new Vector3(0, -lowerLegY, 0);
            knee.Point = new Vector3(0, -thighY, 0);
            hip.Point = new Vector3(s * hipX, -hipY, 0);

            ankle.AddChild(foot);
            ankle.Valid = true;
            knee.AddChild(ankle);
            knee.Valid = true;
            hip.AddChild(knee);
            hip.Valid = true;

            return hip;
        }

        public static IJoint<Vector4, Vector3> CreateHead()
        {
            int headY = 180;
            var headOrientation = new Vector4(0, 0, -100, 0);

            var head = new OrientedJoint<Vector4, Vector3>();
            head.Orientation = headOrientation;
            head.Point = new Vector3(0, headY, 0);
            head.JointType = JointType.HEAD;
            head.Valid = true;

            return head;
        }

        public static IDictionary<JointType, float> Lengths = new Dictionary<JointType, float>
        {
            {JointType.NECK, 600},
            {JointType.HEAD, 200},
            {JointType.HIP_LEFT, 250},
            {JointType.HIP_RIGHT, 250},
            {JointType.KNEE_LEFT, 450},
            {JointType.KNEE_RIGHT, 450},
            {JointType.ANKLE_LEFT, 450},
            {JointType.ANKLE_RIGHT, 450},
            {JointType.FOOT_LEFT, 250},
            {JointType.FOOT_RIGHT, 250},
            {JointType.SHOULDER_LEFT, 300},
            {JointType.SHOULDER_RIGHT, 300},
            {JointType.ELBOW_LEFT, 400},
            {JointType.ELBOW_RIGHT, 400},
            {JointType.WRIST_LEFT, 380},
            {JointType.WRIST_RIGHT, 380},
            {JointType.HAND_LEFT, 50},
            {JointType.HAND_RIGHT, 50},
        };
    }
}
