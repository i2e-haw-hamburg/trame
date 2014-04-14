#include "leap_adapter.hpp"
#include <iostream>

namespace trame {

leap_adapter::leap_adapter() {

}

leap_adapter::~leap_adapter() {

}

joint leap_adapter::get_left_hand() {
    Leap::Frame frame;
    Leap::HandList hands;
    Leap::Hand leftmost;

    if(leap_controller.isConnected()) //controller is a Controller object
    {
        frame = leap_controller.frame(); //The latest frame
        hands = frame.hands();
        leftmost = hands.leftmost();
    }

    joint left = build_hand(leftmost, 1);
    left.type = joint_type::HAND_LEFT;

    return left;
}

joint leap_adapter::get_right_hand() {
    Leap::Frame frame;
    Leap::HandList hands;
    Leap::Hand rightmost;

    if(leap_controller.isConnected()) //controller is a Controller object
    {
        frame = leap_controller.frame(); //The latest frame
        hands = frame.hands();
        rightmost = hands.rightmost();
    }

    joint right = build_hand(rightmost, 2);
    right.type = joint_type::HAND_RIGHT;

    return right;
}

joint leap_adapter::build_hand(Leap::Hand hand, u_int8_t side) {
    joint thumb;
    joint index;
    joint middle;
    joint ring;
    joint little;

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
        Leap::FingerList fingers = hand.fingers();
        if(fingers.count() > 0) {
            Leap::Vector normal = fingers[0].direction();
            Leap::Vector position = fingers[0].tipPosition();

            thumb.point << position[0], position[1], position[2];
            thumb.normal << normal[0], normal[1], normal[2];
        }

    }

    joint hand_joint = joint::create_parent({thumb, index, middle, ring, little});

    if(hand.isValid()) {
        Leap::Vector normal = hand.palmNormal();
        Leap::Vector position = hand.palmPosition();

        hand_joint.normal << normal[0], normal[1], normal[2];

        hand_joint.point << position[0], position[1], position[2];
    }

    return hand_joint;
}

}
