#include "leap_motion.hpp"

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
    // assuming the person stands on 0,0,0 and looks straight to z+
    neck.point << 0, -100, 80;
    neck.normal << 0, 0, 100;

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

    // shoulders relative to neck
    shoulder.point << (s * 250), 50, 0;

    // elbows relative to shoulders
    elbow.point << (s * 50), 280, 60;

    elbow.add_child(std::move(wrist));
    shoulder.add_child(std::move(elbow));

    return shoulder;
}

}
