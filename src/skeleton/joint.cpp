#include "joint.hpp"
#include <iterator>
#include <iostream>
#include <type_traits>
#include <cstdlib>
#include <cstdarg>

using namespace trame;

joint::joint() : type(joint_type::UNSPECIFIED), normal(0,0,0), point(0,0,0) {

}

joint::joint(const joint& j) {
    type = j.type;
    normal = j.normal;
    point = j.point;
    children = j.children;
}

joint::~joint() {
    
}

// TODO: add joints to parent
// TODO: make method static
// TODO: check for loop
joint joint::create_parent(joint j, ...) {
    joint parent;

    va_list list;
    va_start(list, j);

    int sum = 0;
    for( int i = 0 ; i < numargs; i++ )
    {
        joint other_joint = va_arg( listPointer, joint );
    }

    va_end(list);

    return parent;
}

joint::joint(joint&& j) :
    type(j.type), normal(std::move(j.normal)),
    point(std::move(j.point)), children(std::move(j.children))
{

}

joint& joint::operator=(const joint& j) {
    type = j.type;
    normal = j.normal;
    point = j.point;
    children = j.children;

    return *this;
}

joint& joint::operator=(joint&& j) {
    type = j.type;
    normal = std::move(j.normal);
    point = std::move(j.point);
    children = std::move(j.children);
    return *this;
}

bool joint::addChild(joint j) {
    children.push_back(j);
}

bool joint::removeChild(joint_type jt) {
    std::vector<joint>::iterator iter;
    for (iter = children.begin(); iter != children.end(); ++iter) {
        if(iter->type == jt) {
            children.erase(iter);
            return true;
        }
    }

    return false;
}
