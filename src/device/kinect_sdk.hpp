#ifndef TRAME_KINECT_SDK_HPP
#define TRAME_KINECT_SDK_HPP

#include "device_abstraction.hpp"

namespace trame {

class kinect_sdk : public device_abstraction
{
public:
    kinect_sdk();
    kinect_sdk(const kinect_sdk&);
    ~kinect_sdk();

    virtual skeleton get();
};

} // namespace trame

#endif
