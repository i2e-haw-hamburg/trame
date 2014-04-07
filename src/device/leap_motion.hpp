#ifndef TRAME_LEAP_MOTION_HPP
#define TRAME_LEAP_MOTION_HPP

#include "device_abstraction.hpp"
#include "adapter/leap_adapter.hpp"

namespace trame {


class leap_motion : public device_abstraction
{
public:
    leap_motion();
    leap_motion(const leap_motion&);
    ~leap_motion();

    virtual skeleton get();

private:
    joint get_arm(side);
    leap_adapter adapter;
};

} // namespace trame

#endif
