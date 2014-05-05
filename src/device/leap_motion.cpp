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
    skeleton s = skeleton::default_skeleton();

    // get left and right wrist joints from default skeleton
    joint left_wrist, right_wrist;
    left_wrist = s.get_joint(joint_type::WRIST_LEFT);
    right_wrist = s.get_joint(joint_type::WRIST_RIGHT);
    // build left hand with leap
    joint left_hand = adapter.get_left_hand();
    left_wrist.valid = left_hand.valid;
    left_wrist.add_child(std::move(left_hand));
    // build right hand with leap
    joint right_hand = adapter.get_right_hand();
    right_wrist.valid = right_hand.valid;
    right_wrist.add_child(std::move(right_hand));

    // update skeleton
    s.update_joint(joint_type::WRIST_LEFT, left_wrist);
    s.update_joint(joint_type::WRIST_RIGHT, right_wrist);

    s.id = 0;
    s.timestamp = current_time();
    s.valid = left_wrist.valid && right_wrist.valid;

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

    // elbows relative to shoulders
    elbow.point << (s * 50), -280, 60;
    elbow.valid = wrist.valid;
    elbow.add_child(std::move(wrist));

    // shoulders relative to neck
    shoulder.point << (s * 250), 0, 0;
    shoulder.valid = elbow.valid;
    shoulder.add_child(std::move(elbow));

    return shoulder;
}

}
