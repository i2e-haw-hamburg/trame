#ifndef TRAME_SKELETON_JOINT_HPP
#define TRAME_SKELETON_JOINT_HPP

#include <vector>
#include <eigen3/Eigen/Dense>
#include <initializer_list>
#include <iostream>

using namespace std;

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
    vector<joint> children;
    /**
     * The normal vector of a joint.
     * 
     * In default it is been used in head, hands
     * and center. If a joint doesn't provide a normal vector it is set to 
     * (0 0 0).
     */
    Eigen::Vector3d normal;
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

    bool valid;
    /**
     * Add a bew child j to the joint.
     * 
     * @param j the new child
     * @return
     */
    bool add_child(joint j);
    /**
     * @brief removes a joint with a given type from the child joints
     * @param jt the joint, that should be removed from the list of children
     * @return a new
     */
    bool remove_child(joint_type jt);
    /**
     * @brief Find a child joint specified by the joint type.
     *
     * The method doesn't perform a deep search.
     *
     * @param jt the joint type searched for
     * @return if child was found, then the correct joint will be returned
     *  otherwise the method returns an empty joint
     */
    joint find_child(joint_type jt);

    joint deep_find(joint_type jt);

    bool update(joint_type jt, joint j);
    /**
     * @brief Checks if two joints are equal.
     *
     * The normal, point, valid flag, type and children are tested.
     *
     * @param j the other joint
     * @return true if both joints and their descendants are equal
     */
    bool equals(joint &j);
    /**
     * @brief Operator overloading for equality operator.
     *
     * Method uses the equals() method internal.
     *
     * @param rhs the other skeleton
     * @return TRUE if the skeletons are equal.
     */
    bool operator==(joint &rhs);

    /**
     * @brief create a joint with a given list of children.
     * @param list a list of child joints
     * @return a new joint
     */
    static joint create_parent(std::initializer_list<joint> list);
};

} // trame

ostream& operator<<(ostream& ost, const trame::joint &j);


#endif
