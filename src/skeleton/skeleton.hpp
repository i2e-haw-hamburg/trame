#ifndef TRAME_SKELETON_SKELETON_HPP 
#define TRAME_SKELETON_SKELETON_HPP

#include "joint.hpp"

namespace trame {

enum side {
    LEFT = -1,
    RIGHT = 1
};

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
    joint root;
    u_int32_t timestamp;
    u_int32_t id;
};

} // trame


#endif
