#include "leap_adapter.hpp"

namespace trame {

leap_adapter::leap_adapter() {

}

leap_adapter::~leap_adapter() {

}

joint leap_adapter::get_left_hand() {
    joint j;
    j.type = joint_type::HAND_LEFT;
    return j;
}

joint leap_adapter::get_right_hand() {
    joint j;
    j.type = joint_type::HAND_RIGHT;
    return j;
}

}
