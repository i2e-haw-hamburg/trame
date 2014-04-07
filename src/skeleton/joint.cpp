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
