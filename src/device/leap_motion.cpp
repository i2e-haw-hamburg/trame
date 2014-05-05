#include "leap_motion.hpp"

namespace trame
{

leap_motion::leap_motion()
{

}

leap_motion::leap_motion(const leap_motion &l)
{

}

leap_motion::~leap_motion()
{

}

skeleton leap_motion::get()
{
    skeleton s;

    joint neck;
    neck.type = joint_type::NECK;
    // assuming the person stands on 0,0,0 and looks straight to z+
    neck.point << 0, -100, 80;
    neck.normal << 0, 0, 100;
    joint left_arm, right_arm;
    left_arm = get_arm(side::LEFT);
    right_arm = get_arm(side::RIGHT);
    neck.valid = left_arm.valid && right_arm.valid;

    neck.add_child(std::move(left_arm));
    neck.add_child(std::move(right_arm));

    s.root = neck;
    s.valid = neck.valid;
    return s;
}

joint leap_motion::get_arm(side s)
{
    joint shoulder;
    joint elbow;
    joint wrist;

    if(s == side::LEFT) {
        shoulder.type = joint_type::SHOULDER_LEFT;
        elbow.type = joint_type::ELBOW_LEFT;
        wrist.type = joint_type::WRIST_LEFT;
        joint hand = adapter.get_left_hand();
        wrist.valid = hand.valid;
        wrist.add_child(std::move(hand));
    } else {
        shoulder.type = joint_type::SHOULDER_RIGHT;
        elbow.type = joint_type::ELBOW_RIGHT;
        wrist.type = joint_type::WRIST_RIGHT;
        joint hand = adapter.get_right_hand();
        wrist.valid = hand.valid;
        wrist.add_child(std::move(hand));
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
