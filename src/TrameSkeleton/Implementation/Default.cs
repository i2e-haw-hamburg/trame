﻿using System;
using System.Collections.Generic;
using TrameSkeleton.Math;
using Convert = System.Convert;

namespace Trame.Implementation.Skeleton
{
	/// <summary>
	/// Default.
	/// </summary>
    class Default
    {
		/// <summary>
		/// Creates the skeleton.
		/// </summary>
		/// <returns>The skeleton.</returns>
        public static ISkeleton CreateSkeleton()
        {
            int centerY = 1100;
            int upperBodyY = 350;
            var centerOrientation = new Vector4(0, 0, 0, 0);
            var s = new Skeleton { Valid = true };

            var leftShoulder = CreateArm(Side.LEFT);
            var rightShoulder = CreateArm(Side.RIGHT);
            var leftHip = CreateLeg(Side.LEFT);
            var rightHip = CreateLeg(Side.RIGHT);
            var head = CreateHead();

            IJoint neck = Creator.CreateParent(new List<IJoint> { head, rightShoulder, leftShoulder });
            neck.Point = new Vector3(0, upperBodyY, 0);
            neck.JointType = JointType.NECK;
            neck.Valid = true;

            IJoint center = Creator.CreateParent(new List<IJoint> { neck, rightHip, leftHip });
            center.Orientation = centerOrientation;
            center.Point = new Vector3(0, centerY, 0);
            center.JointType = JointType.CENTER;
            center.Valid = true;

            s.Root = center;

            return s;
        }

        public static ISkeleton CreateInMapSkeleton()
        {
            int centerY = 1100;
            int upperBodyY = 350;
            var centerOrientation = new Vector4(0, 0, 0, 0);
            var s = new InMapSkeleton{ Valid = true };

            var leftShoulder = CreateArm(Side.LEFT);
            s.UpdateSkeleton(JointType.SHOULDER_LEFT, leftShoulder);
            s.UpdateSkeleton(JointType.ELBOW_LEFT, leftShoulder.DeepFind(JointType.ELBOW_LEFT));
            s.UpdateSkeleton(JointType.WRIST_LEFT, leftShoulder.DeepFind(JointType.WRIST_LEFT));
            s.UpdateSkeleton(JointType.HAND_LEFT, leftShoulder.DeepFind(JointType.HAND_LEFT));
            var rightShoulder = CreateArm(Side.RIGHT);
            s.UpdateSkeleton(JointType.SHOULDER_RIGHT, rightShoulder);
            s.UpdateSkeleton(JointType.ELBOW_RIGHT, rightShoulder.DeepFind(JointType.ELBOW_RIGHT));
            s.UpdateSkeleton(JointType.WRIST_RIGHT, rightShoulder.DeepFind(JointType.WRIST_RIGHT));
            s.UpdateSkeleton(JointType.HAND_RIGHT, rightShoulder.DeepFind(JointType.HAND_RIGHT));
            var leftHip = CreateLeg(Side.LEFT);
            s.UpdateSkeleton(JointType.HIP_LEFT, leftHip);
            s.UpdateSkeleton(JointType.KNEE_LEFT, leftHip.DeepFind(JointType.KNEE_LEFT));
            s.UpdateSkeleton(JointType.ANKLE_LEFT, leftHip.DeepFind(JointType.ANKLE_LEFT));
            s.UpdateSkeleton(JointType.FOOT_LEFT, leftHip.DeepFind(JointType.FOOT_LEFT));
            var rightHip = CreateLeg(Side.RIGHT);
            s.UpdateSkeleton(JointType.HIP_RIGHT, rightHip);
            s.UpdateSkeleton(JointType.KNEE_RIGHT, rightHip.DeepFind(JointType.KNEE_RIGHT));
            s.UpdateSkeleton(JointType.ANKLE_RIGHT, rightHip.DeepFind(JointType.ANKLE_RIGHT));
            s.UpdateSkeleton(JointType.FOOT_RIGHT, rightHip.DeepFind(JointType.FOOT_RIGHT));
            var head = CreateHead();
            s.UpdateSkeleton(JointType.HEAD, head);

            IJoint neck = Creator.CreateParent(new List<IJoint> { head, rightShoulder, leftShoulder });
            neck.Point = new Vector3(0, upperBodyY, 0);
            neck.JointType = JointType.NECK;
            neck.Valid = true;
            s.UpdateSkeleton(JointType.NECK, neck);

            IJoint center = Creator.CreateParent(new List<IJoint> { neck, rightHip, leftHip });
            center.Orientation = centerOrientation;
            center.Point = new Vector3(0, centerY, 0);
            center.JointType = JointType.CENTER;
            center.Valid = true;
            s.UpdateSkeleton(JointType.CENTER, center);
            s.Root = center;

            return s;
        }

		/// <summary>
		/// Creates the arm.
		/// </summary>
		/// <returns>The arm.</returns>
		/// <param name="side">Side.</param>
        public static IJoint CreateArm(Side side)
        {
            int handLength = 50;
            int forearmX = 50;
            int forearmY = 355;
            int armX = 75;
            int armY = 320;
            int shoulderX = 220;
            var handOrientation = new Vector4(0, 0, 0, 0);

            var shoulder = new OrientedJoint();
            var elbow = new OrientedJoint();
            var wrist = new OrientedJoint();
            var hand = new OrientedJoint();

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
		/// <summary>
		/// Creates the leg.
		/// </summary>
		/// <returns>The leg.</returns>
		/// <param name="side">Side.</param>
        public static IJoint CreateLeg(Side side)
        {
            int footLength = 255;
            int lowerLegY = 410;
            int thighY = 540;
            int hipX = 180;
            int hipY = 100;

            var footOrientation = new Vector4(0, 0, 0, 0);

            var foot = new OrientedJoint();
            var ankle = new OrientedJoint();
            var knee = new OrientedJoint();
            var hip = new OrientedJoint();

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
		/// <summary>
		/// Creates the head.
		/// </summary>
		/// <returns>The head.</returns>
        public static IJoint CreateHead()
        {
            int headY = 180;
            var headOrientation = new Vector4(0, 0, 0, 0);

            var head = new OrientedJoint
            {
                Orientation = headOrientation,
                Point = new Vector3(0, headY, 0),
                JointType = JointType.HEAD,
                Valid = true
            };

            return head;
        }
		/// <summary>
		/// The lengths.
		/// </summary>
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
            {JointType.SHOULDER_LEFT, 220},
            {JointType.SHOULDER_RIGHT, 220},
            {JointType.ELBOW_LEFT, 260},
            {JointType.ELBOW_RIGHT, 260},
            {JointType.WRIST_LEFT, 270},
            {JointType.WRIST_RIGHT, 270},
            {JointType.HAND_LEFT, 180},
            {JointType.HAND_RIGHT, 180},
        };
    }
}
