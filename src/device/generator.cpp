#include "generator.hpp"
#include "kinect_sdk.hpp"

namespace trame {

generator::generator() {
    device = new kinect_sdk;
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
