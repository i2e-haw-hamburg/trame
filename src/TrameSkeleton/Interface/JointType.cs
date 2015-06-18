using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trame
{
    /// <summary>
    /// Joint type.
    /// </summary>
    public enum JointType
    {
        /// <summary>
        /// The UNSPECIFIED
        /// </summary>
        UNSPECIFIED = 0,
        /// <summary>
        /// main body parts
        /// </summary>
        HEAD = 21200,
        NECK = 20100,
        CENTER = 10000,
        SHOULDER_LEFT = 22200,
        SHOULDER_RIGHT = 20200,
        HIP_LEFT = 1100,
        HIP_RIGHT = 100,
        KNEE_LEFT = 1200,
        KNEE_RIGHT = 200,
        ANKLE_LEFT = 1300,
        ANKLE_RIGHT = 300,
        FOOT_LEFT = 1400,
        FOOT_RIGHT = 400,
        ELBOW_LEFT = 22300,
        ELBOW_RIGHT = 20300,
        WRIST_LEFT = 22400,
        WRIST_RIGHT = 20400,
        HAND_LEFT = 22500,
        HAND_RIGHT = 20500,
        // additional body parts like fingers
        THUMB_LEFT = 22601,
        INDEX_FINGER_LEFT = 22602,
        MIDDLE_FINGER_LEFT = 22603,
        RING_FINGER_LEFT = 22604,
        LITTLE_FINGER_LEFT = 22605,
        THUMB_RIGHT = 20601,
        INDEX_FINGER_RIGHT = 20602,
        MIDDLE_FINGER_RIGHT = 20603,
        RING_FINGER_RIGHT = 20604,
        LITTLE_FINGER_RIGHT = 20605
    }
    
    public static class Extension
    {
        /// <summary>
        /// To the int.
        /// </summary>
        /// <returns>The int.</returns>
        /// <param name="jt">Jt.</param>
        public static int ToInt(this JointType jt)
        {
            return (int) Convert.ChangeType(jt, TypeCode.Int32);
        }
    }
}
