#include "leap_adapter.hpp"
#include <iostream>

namespace trame
{

leap_adapter::leap_adapter()
{

}

leap_adapter::~leap_adapter()
{

}

joint leap_adapter::get_left_hand(joint wrist)
{
    Leap::Frame frame;
    Leap::HandList hands;
    Leap::Hand leftmost;

    if(leap_controller.isConnected()) //controller is a Controller object
    {
        frame = leap_controller.frame(); //The latest frame
        hands = frame.hands();
        leftmost = hands.leftmost();
    }

    joint left = build_hand(leftmost, 1, wrist);
    left.type = joint_type::HAND_LEFT;

    return left;
}

joint leap_adapter::get_right_hand(joint wrist)
{
    Leap::Frame frame;
    Leap::HandList hands;
    Leap::Hand rightmost;

    if(leap_controller.isConnected()) //controller is a Controller object
    {
        frame = leap_controller.frame(); //The latest frame
        hands = frame.hands();
        rightmost = hands.rightmost();
    }

    joint right = build_hand(rightmost, 2, wrist);
    right.type = joint_type::HAND_RIGHT;

    return right;
}

joint leap_adapter::build_hand(Leap::Hand hand, u_int8_t side, joint wrist)
{
    joint thumb;
    joint index;
    joint middle;
    joint ring;
    joint little;
    joint hand_joint;

    if(side == 1) {
        thumb.type = joint_type::THUMB_LEFT;
        index.type = joint_type::INDEX_FINGER_LEFT;
        middle.type = joint_type::MIDDLE_FINGER_LEFT;
        ring.type = joint_type::RING_FINGER_LEFT;
        little.type = joint_type::LITTLE_FINGER_LEFT;
    } else {
        thumb.type = joint_type::THUMB_RIGHT;
        index.type = joint_type::INDEX_FINGER_RIGHT;
        middle.type = joint_type::MIDDLE_FINGER_RIGHT;
        ring.type = joint_type::RING_FINGER_RIGHT;
        little.type = joint_type::LITTLE_FINGER_RIGHT;
    }

    if(hand.isValid()) {
        Leap::Vector normal = hand.palmNormal();
        Leap::Vector hand_position = hand.palmPosition();

        hand_joint.normal << 100 * normal[0], 100 * normal[1], 100 * normal[2];
        hand_joint.point << 0, 0, -100;

        Leap::FingerList fingers = hand.fingers();
        if(fingers.count() > 0) {
            Leap::Vector normal = fingers[0].direction();
            Leap::Vector position = fingers[0].tipPosition() - hand_position;

            thumb.point << position[0], position[1], position[2];
            thumb.normal << 100 * normal[0], 100 * normal[1], 100 * normal[2];
            hand_joint.add_child(thumb);
        }

        hand_joint.valid = true;
        //hand_joint.add_child(index);
        //hand_joint.add_child(middle);
        //hand_joint.add_child(ring);
        //hand_joint.add_child(little);
    }

    return hand_joint;
}

}
