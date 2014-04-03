#include "generator.hpp"
#include "kinect_sdk.hpp"

namespace trame {

generator::generator() {
    device = new kinect_sdk;
}

generator::generator(const generator&) {

}

generator::~generator() {

}

skeleton generator::get_next() {
    return device->get();
}
}
