#include "kinect_sdk.hpp"
#include "../skeleton/skeleton.hpp"

namespace trame {

kinect_sdk::kinect_sdk() {
}

kinect_sdk::kinect_sdk(const kinect_sdk&) {

}

kinect_sdk::~kinect_sdk() {

}

skeleton kinect_sdk::get() {
    return skeleton();
}

}
