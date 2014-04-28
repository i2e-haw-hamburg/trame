#ifndef TRAME_SKELETON_SKELETON_HPP 
#define TRAME_SKELETON_SKELETON_HPP

#include "joint.hpp"

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
};

} // trame


#endif
