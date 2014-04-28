#include "generator.hpp"
#include "kinect_sdk.hpp"
#include "leap_motion.hpp"
#include "skeleton_mockup.hpp"

namespace trame
{

generator::generator()
{
    device = new skeleton_mockup;
}

generator::generator(const generator&)
{

}

generator::~generator()
{
    delete device;
}

skeleton generator::get_next()
{
    return device->get();
}

}
