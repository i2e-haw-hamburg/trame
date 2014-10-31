#ifndef TRAME_LEAP_ADAPTER_HPP
#define TRAME_LEAP_ADAPTER_HPP

#include <leap/Leap.h>
#include "../../skeleton/joint.hpp"
#include <leap/Leap.h>

namespace trame {

class leap_adapter
{
public:
    leap_adapter();
    ~leap_adapter();

    joint get_left_hand(joint wrist);
    joint get_right_hand(joint wrist);

private:
    joint build_hand(Leap::Hand, u_int8_t, joint wrist);

    Leap::Controller leap_controller;
};

} // namespace trame

#endif
