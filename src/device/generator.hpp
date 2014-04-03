#ifndef TRAME_LEAP_MOTION_HPP
#define TRAME_LEAP_MOTION_HPP

#include "../skeleton/skeleton.hpp"
#include "device_abstraction.hpp"

namespace trame {

class generator
{
public:
    generator();
    generator(const generator&);
    ~generator();

    virtual skeleton get_next();

private:
    device_abstraction *device;
};

} // namespace trame

#endif
