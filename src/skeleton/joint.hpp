#ifndef TRAME_SKELETON_JOINT_HPP
#define TRAME_SKELETON_JOINT_HPP



namespace trame {

class joint
{

public:
    joint();
    joint(const joint&);
    ~joint();

public:
    std::vector<joint> children;
    point normale;
    point point;
    joint_type type;
};

enum joint_type
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
    FOOT_LEFT,
    FOOT_RIGHT
    ELBOW_LEFT,
    ELBOW_RIGHT,
    WRIST_LEFT,
    WIRST_RIGHT,
    HAND_LEFT,
    HAND_RIGHT
    // additional body parts like fingers

};

} // trame

#endif