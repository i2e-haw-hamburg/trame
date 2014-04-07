#ifndef TRAME_SKELETON_JOINT_HPP
#define TRAME_SKELETON_JOINT_HPP

#include <vector>
#include <eigen3/Eigen/Dense>

namespace trame {

enum class joint_type : u_int32_t
{
    UNSPECIFIED = 0,
    // main body parts
    HEAD = 1,
    NECK = 5,
    SHOULDER_LEFT = 111,
    SHOULDER_RIGHT = 121,
    HIP_LEFT = 211,
    HIP_RIGHT = 221,
    KNEE_LEFT = 212,
    KNEE_RIGHT = 222,
    ANKLE_LEFT = 213,
    ANKLE_RIGHT = 223,
    FOOT_LEFT = 214,
    FOOT_RIGHT = 224,
    ELBOW_LEFT = 112,
    ELBOW_RIGHT = 122,
    WRIST_LEFT = 113,
    WRIST_RIGHT = 123,
    HAND_LEFT = 114,
    HAND_RIGHT = 124
    // additional body parts like fingers
};

class joint
{

public:
    joint();
    joint(const joint&);
    ~joint();

public:
    std::vector<joint> children;
    Eigen::Vector3d  normal;
    Eigen::Vector3d  point;
    joint_type type;

    bool addChild(joint);
    bool removeChild(joint_type);
};



} // trame

#endif
