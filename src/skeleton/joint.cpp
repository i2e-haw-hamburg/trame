#include "joint.hpp"
#include <iterator>
#include <iostream>
#include <type_traits>
#include <cstdlib>

namespace trame
{

joint::joint() : type(joint_type::UNSPECIFIED), normal(0,0,0), point(0,0,0),
    valid(false)
{

}

joint::joint(const joint& j)
{
    type = j.type;
    normal = j.normal;
    point = j.point;
    children = j.children;
    valid = j.valid;
}

joint::~joint()
{
    
}

joint joint::create_parent(std::initializer_list<joint> list)
{
    joint parent;

    for(auto& el : list)
    {
        parent.add_child(el);
    }

    return parent;
}

joint::joint(joint&& j) :
    type(j.type), normal(std::move(j.normal)), valid(j.valid),
    point(std::move(j.point)), children(std::move(j.children))
{

}

joint& joint::operator=(const joint& j)
{
    type = j.type;
    normal = j.normal;
    point = j.point;
    children = j.children;
    valid = j.valid;

    return *this;
}

joint& joint::operator=(joint&& j)
{
    type = j.type;
    normal = std::move(j.normal);
    point = std::move(j.point);
    valid = j.valid;
    children = std::move(j.children);
    return *this;
}

bool joint::add_child(joint j)
{
    children.push_back(j);
}

joint joint::find_child(joint_type jt)
{
    std::vector<joint>::iterator iter;
    for (iter = children.begin(); iter != children.end(); ++iter) {
        if(iter->type == jt) {
            return *iter;
        }
    }

    return joint();
}

joint joint::deep_find(joint_type jt)
{
    joint j = find_child(jt);
    if(j.type != jt) {
        std::vector<joint>::iterator iter;
        for (iter = children.begin(); iter != children.end(); ++iter) {
            j = iter->deep_find(jt);
            if(j.type == jt) {
                break;
            }
        }
    }

    return j;
}

bool joint::update(joint_type jt, joint j)
{
    bool result = false;

    if(find_child(jt).type != jt) {
        std::vector<joint>::iterator iter;
        for (iter = children.begin(); iter != children.end(); ++iter) {
            result = iter->update(jt, j);
        }
    } else {
        remove_child(jt);
        add_child(j);
        result = true;
    }

    return result;
}

bool joint::remove_child(joint_type jt)
{
    std::vector<joint>::iterator iter;
    for (iter = children.begin(); iter != children.end(); ++iter) {
        if(iter->type == jt) {
            children.erase(iter);
            return true;
        }
    }

    return false;
}

bool joint::equals(joint &j)
{
    for(auto& child : children) {
        joint oc = j.find_child(child.type);
        if(!(child == oc)) {
            return false;
        }
    }

    return valid == j.valid && type == j.type && normal == j.normal
            && point == j.point;
}

bool joint::operator==(joint &rhs)
{
    return equals(rhs);
}

}
