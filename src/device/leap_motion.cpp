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
    joint left_hand = adapter.get_left_hand(left_wrist);
    left_wrist.valid = left_hand.valid;
    left_wrist.update(joint_type::HAND_LEFT, std::move(left_hand));
    // build right hand with leap
    joint right_hand = adapter.get_right_hand(right_wrist);
    right_wrist.valid = right_hand.valid;
    right_wrist.update(joint_type::HAND_RIGHT, std::move(right_hand));

    // update skeleton
    s.update_joint(joint_type::WRIST_LEFT, left_wrist);
    s.update_joint(joint_type::WRIST_RIGHT, right_wrist);

    s.id = 0;
    s.timestamp = current_time();
    s.valid = left_wrist.valid && right_wrist.valid;

    return s;
}

}
