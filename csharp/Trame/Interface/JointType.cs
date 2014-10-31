using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trame
{
    public enum JointType
    {
        UNSPECIFIED = 0,
        // main body parts
        HEAD = 1000,
        NECK = 1200,
        CENTER = 2000,
        SHOULDER_LEFT = 1110,
        SHOULDER_RIGHT = 1210,
        HIP_LEFT = 2110,
        HIP_RIGHT = 2210,
        KNEE_LEFT = 2120,
        KNEE_RIGHT = 2220,
        ANKLE_LEFT = 2130,
        ANKLE_RIGHT = 2230,
        FOOT_LEFT = 2140,
        FOOT_RIGHT = 2240,
        ELBOW_LEFT = 1120,
        ELBOW_RIGHT = 1220,
        WRIST_LEFT = 1130,
        WRIST_RIGHT = 1230,
        HAND_LEFT = 1140,
        HAND_RIGHT = 1240,
        // additional body parts like fingers
        THUMB_LEFT = 1141,
        INDEX_FINGER_LEFT = 1142,
        MIDDLE_FINGER_LEFT = 1143,
        RING_FINGER_LEFT = 1144,
        LITTLE_FINGER_LEFT = 1145,
        THUMB_RIGHT = 1241,
        INDEX_FINGER_RIGHT = 1242,
        MIDDLE_FINGER_RIGHT = 1243,
        RING_FINGER_RIGHT = 1244,
        LITTLE_FINGER_RIGHT = 1245
    }
}
