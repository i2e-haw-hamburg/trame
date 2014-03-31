#ifndef TRAME_SKELETON_JOINT_HPP
#define TRAME_SKELETON_JOINT_HPP

#include <vector>
#include <eigen3/Eigen/Dense>

namespace trame {

enum class joint_type
{
    UNSPECIFIED,
    // main body parts
    HEAD,
    NECK,
    SHOULDER_LEFT,
    SHOULDER_RIGHT,
    HIP_LEFT,
    HIP_RIGHT,
    KNEE_LEFT,
    KNEE_RIGHT,
    ANKLE_LEFT,
    ANKLE_RIGHT,
    FOOT_LEFT,
    FOOT_RIGHT,
    ELBOW_LEFT,
    ELBOW_RIGHT,
    WRIST_LEFT,
    WIRST_RIGHT,
    HAND_LEFT,
    HAND_RIGHT
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
