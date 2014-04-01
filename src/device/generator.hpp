#ifndef TRAME_LEAP_MOTION_HPP
#define TRAME_LEAP_MOTION_HPP

#include "../skeleton/skeleton.hpp"

namespace trame {

class generator
{
public:
    generator();
    generator(const generator&);
    ~generator();

    virtual skeleton get_next() = 0;
};

} // namespace trame

#endif
