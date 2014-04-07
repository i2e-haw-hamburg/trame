#include "generator.hpp"
#include "kinect_sdk.hpp"
#include "leap_motion.hpp"
#include "device_stub.hpp"

namespace trame {

generator::generator() {
    device = new leap_motion;
}

generator::generator(const generator&) {

}

generator::~generator() {
    delete device;
}

skeleton generator::get_next() {
    return device->get();
}
}
