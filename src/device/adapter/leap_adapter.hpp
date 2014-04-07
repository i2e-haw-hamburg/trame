#ifndef TRAME_LEAP_ADAPTER_HPP
#define TRAME_LEAP_ADAPTER_HPP

#include <leap/Leap.h>
#include "../../skeleton/joint.hpp"

namespace trame {

class leap_adapter
{
public:
    leap_adapter();
    ~leap_adapter();

    joint get_left_hand();
    joint get_right_hand();
};

} // namespace trame

#endif
