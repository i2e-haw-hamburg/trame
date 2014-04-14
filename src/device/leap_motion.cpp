#include "leap_motion.hpp"
#include "../skeleton/skeleton.hpp"

namespace trame {

leap_motion::leap_motion() {

}

leap_motion::leap_motion(const leap_motion &l) {

}

leap_motion::~leap_motion() {

}

skeleton leap_motion::get() {
    skeleton s;

    joint neck;
    neck.type = joint_type::NECK;

    neck.add_child(std::move(get_arm(side::LEFT)));
    neck.add_child(std::move(get_arm(side::RIGHT)));

    s.root = neck;
    return s;
}

joint leap_motion::get_arm(side s) {
    joint shoulder;
    joint elbow;
    joint wrist;

    if(s == side::LEFT) {
        shoulder.type = joint_type::SHOULDER_LEFT;
        elbow.type = joint_type::ELBOW_LEFT;
        wrist.type = joint_type::WRIST_LEFT;
        wrist.add_child(std::move(adapter.get_left_hand()));
    } else {
        shoulder.type = joint_type::SHOULDER_RIGHT;
        elbow.type = joint_type::ELBOW_RIGHT;
        wrist.type = joint_type::WRIST_RIGHT;
        wrist.add_child(std::move(adapter.get_right_hand()));
    }

    elbow.add_child(std::move(wrist));
    shoulder.add_child(std::move(elbow));

    return shoulder;
}

}
