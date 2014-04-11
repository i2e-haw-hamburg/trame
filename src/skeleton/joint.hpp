#ifndef TRAME_SKELETON_JOINT_HPP
#define TRAME_SKELETON_JOINT_HPP

#include <vector>
#include <eigen3/Eigen/Dense>

namespace trame {
/**
 * List of joint types.
 * Convention for multi digit numbers is that the first digit defines the body 
 * part  (eg. arms 1, legs 2), the second digit indicates the side of the joint
 * (left 1, right 2). The next digits are unique in the body part and should 
 * start from 1.
 *
 * @author Christian Blank <christian.blank@haw-hamburg.de>
 */
enum class joint_type : u_int32_t
{
    UNSPECIFIED = 0,
    // main body parts
    HEAD = 1,
    NECK = 5,
    CENTER = 9,
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

/**
 * Joint element to use in skeleton. Every joint consists of zero or more 
 * children, a type, a point vector and if needed a normal vector.
 *
 * @author Christian Blank <christian.blank@haw-hamburg.de>
 */
class joint
{

public:
    joint();
    joint(const joint&);
    ~joint();
    /**
     * Move constructor for joints.
     */
    joint(joint&& j);
    /**
     * Operator overloading for assignment with copy.
     */
    joint& operator=(const joint&);
    /**
     * Operator overloading for assignment with move.
     */
    joint& operator=(joint&&);

public:
    /**
     * List of children of a joint.
     * 
     * Use this only for iterations. To add or remove a child, use 
     * {@see addChild} and {@see removeChild}.
     */
    std::vector<joint> children;
    /**
     * The normal vector of a joint.
     * 
     * In default it is been used in head, hands
     * and center. If a joint doesn't provide a normal vector it is set to 
     * (0 0 0).
     */
    Eigen::Vector3d  normal;
    /**
     * The point vector of a joint.
     * 
     * If the joint is the root of a skeleton the joint is absolute, 
     * otherwise the join is relative to the parent joint.
     */
    Eigen::Vector3d  point;
    /**
     * The type of a joint.
     */
    joint_type type;
    /**
     * The point vector of a joint.
     * 
     * If the joint is the root of a skeleton the joint is absolute, 
     * 
     * @param joint
     * @return
     */
    bool addChild(joint);
    bool removeChild(joint_type);
};



} // trame

#endif
