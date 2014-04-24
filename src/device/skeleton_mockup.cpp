#include "skeleton_mockup.hpp"
#include <sys/time.h>

namespace trame {

skeleton_mockup::skeleton_mockup() :is_dwarf(true) {

}

skeleton_mockup::skeleton_mockup(const skeleton_mockup&) {

}

skeleton_mockup::~skeleton_mockup() {

}

skeleton skeleton_mockup::get() {
    struct timeval time;
    gettimeofday(&time,NULL);
    static int id = 0;
    skeleton s;
    s.id = id;
    s.timestamp = (int) (time.tv_usec / 1000 + time.tv_sec * 1000) % (24*60*60*1000);

    if(is_dwarf) {

    } else {

    }
    // left arm
    joint left_hand;
    left_hand.normal << 10, 0, 0;
    left_hand.point << 50, -50, 0;
    left_hand.type = joint_type::HAND_LEFT;

    joint left_wrist = joint::create_parent({left_hand});
    left_wrist.point << 50, -455, 0;
    left_wrist.type = joint_type::WRIST_LEFT;
    joint left_elbow = joint::create_parent({left_wrist});
    left_elbow.point << -75, -320, 0;
    left_elbow.type = joint_type::ELBOW_LEFT;
    joint left_shoulder = joint::create_parent({left_elbow});
    left_shoulder.point << -220, 0, 0;
    left_shoulder.type = joint_type::SHOULDER_LEFT;

    // right arm
    joint right_hand;
    right_hand.normal << -10, 0, 0;
    right_hand.point << -50, -50, 0;
    right_hand.type = joint_type::HAND_RIGHT;

    joint right_wrist = joint::create_parent({right_hand});
    right_wrist.point << -50, -455, 0;
    right_wrist.type = joint_type::WRIST_RIGHT;
    joint right_elbow = joint::create_parent({right_wrist});
    right_elbow.point << 75, -320, 0;
    right_elbow.type = joint_type::ELBOW_RIGHT;
    joint right_shoulder = joint::create_parent({right_elbow});
    right_shoulder.point << 220, 0, 0;
    right_shoulder.type = joint_type::SHOULDER_RIGHT;

    // left foot
    joint left_foot;
    left_foot.normal << 0, 0, 10;
    left_foot.point << -20, 0, -255;
    left_foot.type = joint_type::FOOT_LEFT;

    joint left_ankle = joint::create_parent({left_foot});
    left_ankle.point << 0, -410, 0;
    left_ankle.type = joint_type::ANKLE_LEFT;
    joint left_knee = joint::create_parent({left_ankle});
    left_knee.point << 0, -540, 0;
    left_knee.type = joint_type::KNEE_LEFT;
    joint left_hip = joint::create_parent({left_knee});
    left_hip.point << -180, -100, 0;
    left_hip.type = joint_type::HIP_LEFT;

    // right foot
    joint right_foot;
    right_foot.normal << 0, 0, -10;
    right_foot.point << -20, 0, -255;
    right_foot.type = joint_type::FOOT_RIGHT;

    joint right_ankle = joint::create_parent({right_foot});
    right_ankle.point << 0, -410, 0;
    right_ankle.type = joint_type::ANKLE_RIGHT;
    joint right_knee = joint::create_parent({right_ankle});
    right_knee.point << 0, -540, 0;
    right_knee.type = joint_type::KNEE_RIGHT;
    joint right_hip = joint::create_parent({right_knee});
    right_hip.point << 180, -100, 0;
    right_hip.type = joint_type::HIP_RIGHT;

    joint head;
    head.normal << 0, 0, 10;
    head.point << 0, 180, 0;
    head.type = joint_type::HEAD;

    joint neck = joint::create_parent({head, right_shoulder,
                                        left_shoulder});
    neck.point << 0, 350, 0;
    neck.type = joint_type::NECK;

    joint center = joint::create_parent({neck, right_hip, left_hip});
    center.normal << 0, 0, 10;
    center.point << 0, 1100, 0;
    center.type = joint_type::CENTER;
    s.root = center;

    return std::move(s);
}

}
