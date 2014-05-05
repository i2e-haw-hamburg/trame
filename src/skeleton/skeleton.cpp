#include "skeleton.hpp"
#include <iostream>

namespace trame
{

skeleton::skeleton() : timestamp(0),id(0)
{
}

skeleton::skeleton(const skeleton& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;
}

skeleton::~skeleton()
{
    
}

skeleton::skeleton(skeleton&& s) :
    timestamp(s.timestamp), id(s.id),
    root(std::move(s.root))
{

}

skeleton& skeleton::operator=(const skeleton& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;

    return *this;
}

skeleton& skeleton::operator=(skeleton&& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = std::move(s.root);
    return *this;
}

skeleton skeleton::default_skeleton()
{
    int hand_length = 50;
    int forearm_x = 50;
    int forearm_y = 455;
    int arm_x = 75;
    int arm_y = 320;
    int shoulder_x = 220;

    int foot_length = 255;
    int lower_leg_y = 410;
    int thigh_y = 540;
    int hip_x = 180;
    int hip_y = 100;

    int head_y = 180;
    int center_y = 1100;
    int upper_body_y = 350;

    Eigen::Vector3d foot_normal;
    foot_normal << 0, 0, -100;
    Eigen::Vector3d hand_normal;
    hand_normal << 100, 0, 0;
    Eigen::Vector3d head_normal;
    head_normal << 0, 0, -100;
    Eigen::Vector3d center_normal;
    center_normal << 0, 0, -100;

    skeleton s;
    // left arm
    joint left_hand;
    left_hand.normal = hand_normal;
    left_hand.point << 0, -hand_length, 0;
    left_hand.type = joint_type::HAND_LEFT;

    joint left_wrist = joint::create_parent({left_hand});
    left_wrist.point << forearm_x, -forearm_y, 0;
    left_wrist.type = joint_type::WRIST_LEFT;
    joint left_elbow = joint::create_parent({left_wrist});
    left_elbow.point << -arm_x, -arm_y, 0;
    left_elbow.type = joint_type::ELBOW_LEFT;
    joint left_shoulder = joint::create_parent({left_elbow});
    left_shoulder.point << -shoulder_x, 0, 0;
    left_shoulder.type = joint_type::SHOULDER_LEFT;

    // right arm
    joint right_hand;
    right_hand.normal = -hand_normal;
    right_hand.point << 0, -hand_length, 0;
    right_hand.type = joint_type::HAND_RIGHT;

    joint right_wrist = joint::create_parent({right_hand});
    right_wrist.point << -forearm_x, -forearm_y, 0;
    right_wrist.type = joint_type::WRIST_RIGHT;
    joint right_elbow = joint::create_parent({right_wrist});
    right_elbow.point << arm_x, -arm_y, 0;
    right_elbow.type = joint_type::ELBOW_RIGHT;
    joint right_shoulder = joint::create_parent({right_elbow});
    right_shoulder.point << shoulder_x, 0, 0;
    right_shoulder.type = joint_type::SHOULDER_RIGHT;

    // left foot
    joint left_foot;
    left_foot.normal = foot_normal;
    left_foot.point << 0, 0, -foot_length;
    left_foot.type = joint_type::FOOT_LEFT;

    joint left_ankle = joint::create_parent({left_foot});
    left_ankle.point << 0, -lower_leg_y, 0;
    left_ankle.type = joint_type::ANKLE_LEFT;
    joint left_knee = joint::create_parent({left_ankle});
    left_knee.point << 0, -thigh_y, 0;
    left_knee.type = joint_type::KNEE_LEFT;
    joint left_hip = joint::create_parent({left_knee});
    left_hip.point << -hip_x, -hip_y, 0;
    left_hip.type = joint_type::HIP_LEFT;

    // right foot
    joint right_foot;
    right_foot.normal = foot_normal;
    right_foot.point << 0, 0, -foot_length;
    right_foot.type = joint_type::FOOT_RIGHT;

    joint right_ankle = joint::create_parent({right_foot});
    right_ankle.point << 0, -lower_leg_y, 0;
    right_ankle.type = joint_type::ANKLE_RIGHT;
    joint right_knee = joint::create_parent({right_ankle});
    right_knee.point << 0, -thigh_y, 0;
    right_knee.type = joint_type::KNEE_RIGHT;
    joint right_hip = joint::create_parent({right_knee});
    right_hip.point << hip_x, -hip_y, 0;
    right_hip.type = joint_type::HIP_RIGHT;

    joint head;
    head.normal = head_normal;
    head.point << 0, head_y, 0;
    head.type = joint_type::HEAD;

    joint neck = joint::create_parent({head, right_shoulder,
                                        left_shoulder});
    neck.point << 0, upper_body_y, 0;
    neck.type = joint_type::NECK;

    joint center = joint::create_parent({neck, right_hip, left_hip});
    center.normal = center_normal;
    center.point << 0, center_y, 0;
    center.type = joint_type::CENTER;
    s.root = center;

    return std::move(s);
}

}
