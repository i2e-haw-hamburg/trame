#ifndef TRAME_SKELETON_SKELETON_HPP 
#define TRAME_SKELETON_SKELETON_HPP

#include "joint.hpp"

namespace trame {

class skeleton
{
public:
    skeleton();
    ~skeleton();
    skeleton(const skeleton&);

public:
    joint root;
    long timestamp;
    int id;
};

} // trame


#endif