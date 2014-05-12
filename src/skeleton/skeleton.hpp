#ifndef TRAME_SKELETON_SKELETON_HPP 
#define TRAME_SKELETON_SKELETON_HPP

#include "joint.hpp"
#include <iostream>

using namespace std;

namespace trame
{
/**
 * @brief specifies the side of the skeleton
 */
enum side {
    LEFT = -1,
    RIGHT = 1
};
/**
 * @brief indicates the body part of a skeleton
 */
enum body_part {
    UPPER = 1,
    LOWER = 2
};

class skeleton
{
public:
    skeleton();
    ~skeleton();
    skeleton(const skeleton&);
    /**
     * Move constructor for skeletons.
     */
    skeleton(skeleton&&);
    /**
     * Operator overloading for assignment with copy.
     */
    skeleton& operator=(const skeleton&);
    /**
     * Operator overloading for assignment with move.
     */
    skeleton& operator=(skeleton&&);
    /**
     * @brief Checks if two skeletons are equal.
     *
     * Only the joints and the valid flag are tested.
     *
     * @param s the other skeleton
     * @return
     */
    bool equals(skeleton &s);
    /**
     * @brief Operator overloading for equality operator.
     *
     * Method uses the equals() method internal.
     *
     * @param rhs the other skeleton
     * @return TRUE if the skeletons are equal.
     */
    bool operator==(skeleton &rhs);

    bool update_joint(joint_type jt, joint j);

    joint get_joint(joint_type jt);

public:
    /**
     * @brief Returns the skeleton of and average male standing in a relaxed
     * position.
     *
     * The skeleton data of the proportions are based on the research in
     * http://www.baua.de/cae/servlet/contentblob/698984/publicationFile/46852/Fb1023.pdf
     *
     * @return a skeleton with average proportions
     */
    static skeleton default_skeleton();
    /**
     * @brief create_arm
     * @param s
     * @return
     * @todo write documentation
     */
    static joint create_arm(side s);
    /**
     * @brief create_leg
     * @param s
     * @return
     * @todo write documentation
     */
    static joint create_leg(side s);
    /**
     * @brief create_head
     * @return
     * @todo write documentation
     */
    static joint create_head();
public:
    /**
     * @brief the root point of a skeleton
     *
     * This joint is the only absolute point in the skeleton.
     */
    joint root;
    /**
     * @brief the timestamp of the creation of the skeleton.
     *
     * Remind, that this is not the time when the skeleton is serialized.
     */
    u_int32_t timestamp;
    /**
     * @brief an identifier for the skeleton
     *
     * Implementation specific identifier. Could be used to track skeletons over
     * a series of frames.
     */
    u_int32_t id;
    /**
     * @brief specifies if a skeleton is valid or not
     */
    bool valid;

private:

};

} // trame

ostream& operator<<(ostream& ost, const trame::skeleton &s);

#endif
