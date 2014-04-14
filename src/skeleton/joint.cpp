#include "joint.hpp"
#include <iterator>
#include <iostream>
#include <type_traits>
#include <cstdlib>

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

joint joint::create_parent(std::initializer_list<joint> list) {
    joint parent;

    for(auto& el : list)
    {
        parent.add_child(el);
    }

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

bool joint::add_child(joint j) {
    children.push_back(j);
}

bool joint::remove_child(joint_type jt) {
    std::vector<joint>::iterator iter;
    for (iter = children.begin(); iter != children.end(); ++iter) {
        if(iter->type == jt) {
            children.erase(iter);
            return true;
        }
    }

    return false;
}
