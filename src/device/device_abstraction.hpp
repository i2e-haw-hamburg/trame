#ifndef TRAME_DEVICE_ABSTRACTION_HPP
#define TRAME_DEVICE_ABSTRACTION_HPP

#include "../skeleton/skeleton.hpp"

namespace trame {

class device_abstraction
{
protected:
    device_abstraction();
    device_abstraction(const device_abstraction&);
    ~device_abstraction();

public:
    virtual skeleton get() = 0;
};

} // namespace trame

#endif
