#include "skeleton_mockup.hpp"
#include <iostream>

namespace trame
{

skeleton_mockup::skeleton_mockup() : is_dwarf(true)
{

}

skeleton_mockup::skeleton_mockup(const skeleton_mockup&)
{

}

skeleton_mockup::~skeleton_mockup()
{

}
/**
 * @todo implement variable height for dwarfs
 * @return a skeleton mockup
 */
skeleton skeleton_mockup::get()
{
    static int id = 0;
    id += 1;

    if(is_dwarf) {

    } else {

    }

    skeleton s = skeleton::default_skeleton();

    s.id = id;
    s.timestamp = current_time();

    return std::move(s);
}

}
