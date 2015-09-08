using System;
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
            int centerY = 0;
            int neckY = 400;
            var centerOrientation = new Vector4(0, 0, 0, 0);
            var s = new Skeleton { Valid = true };

            var head = CreateHead();

            IJoint neck = Creator.CreateParent(new List<IJoint> { head });
            neck.Point = new Vector3(0, neckY, 0);
            neck.JointType = JointType.NECK;
            neck.Valid = true;

            IJoint center = Creator.CreateParent(new List<IJoint> { neck });
            center.Orientation = centerOrientation;
            center.Point = new Vector3(0, centerY, 0);
            center.JointType = JointType.CENTER;
            center.Valid = true;

            s.Root = center;

            return s;
        }

        public static ISkeleton CreateInMapSkeleton()
        {
            int centerY = 0;
            int neckY = 400;
            var centerOrientation = new Vector4(0, 0, 0, 0);
            var s = new InMapSkeleton{ Valid = true };

            foreach (var joint in CreateArm(Side.LEFT))
            {
                s.Add(joint);
            }

            foreach (var joint in CreateArm(Side.RIGHT))
            {
                s.Add(joint);
            }

            foreach (var joint in CreateLeg(Side.LEFT))
            {
                s.Add(joint);
            }

            foreach (var joint in CreateLeg(Side.RIGHT))
            {
                s.Add(joint);
            }

            s.Add(CreateHead());
            s.Add(new OrientedJoint(JointType.NECK, true) { Point = new Vector3(0, neckY, 0) });
            s.Add(new OrientedJoint(JointType.CENTER, true) { Point = new Vector3(0, centerY, 0), Orientation = centerOrientation });
            s.Root = s.GetJoint(JointType.CENTER);

            return s;
        }

		/// <summary>
		/// Creates the arm.
		/// </summary>
		/// <returns>The arm.</returns>
		/// <param name="side">Side.</param>
        public static IList<IJoint> CreateArm(Side side)
        {
            int handY = -50;
            int wristX = 50;
            int wristY = 0;
            int elbowX = 75;
            int elbowY = 218;
            int shoulderX = 160;
            var handOrientation = new Vector4(0, 0, 0, 0);

		    var arm = new List<IJoint>();

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

            // shoulders relative to neck
            shoulder.Point = new Vector3(s * shoulderX, 400, 0);
            shoulder.Valid = true;
            arm.Add(shoulder);
            
            // elbows relative to shoulders
            elbow.Point = new Vector3(s * (elbowX + shoulderX), elbowY, 0);
            elbow.Valid = true;
            arm.Add(elbow);

            wrist.Point = new Vector3(s * (wristX + shoulderX), wristY, 0);
            wrist.Valid = true;
            arm.Add(wrist);

            hand.Orientation = handOrientation * -s;
            hand.Point = new Vector3(s * (wristX + shoulderX), handY, 0);
            hand.Valid = true;
            arm.Add(hand);
            
            return arm;
        }
		/// <summary>
		/// Creates the leg.
		/// </summary>
		/// <returns>The leg.</returns>
		/// <param name="side">Side.</param>
        public static IList<IJoint> CreateLeg(Side side)
        {
            int footLength = 255;
            int ankleY = -830;
            int kneeY = -427;
            int hipX = 50;
            int hipY = -100;

            var leg = new List<IJoint>();
            
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

            hip.Point = new Vector3(s * hipX, hipY, 0);
            hip.Valid = true;
            leg.Add(hip);

            knee.Point = new Vector3(s * hipX, kneeY, 0);
            knee.Valid = true;
            leg.Add(knee);

            ankle.Point = new Vector3(s * hipX, ankleY, 0);
            ankle.Valid = true;
            leg.Add(ankle);

            foot.Orientation = footOrientation;
            foot.Point = new Vector3(s * hipX, ankleY, -footLength);
            foot.Valid = true;
        
            return leg;
        }
		/// <summary>
		/// Creates the head.
		/// </summary>
		/// <returns>The head.</returns>
        public static IJoint CreateHead()
        {
            int headY = 580;
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
