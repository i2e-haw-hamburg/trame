#include "skeleton.hpp"
#include <iostream>

namespace trame
{

skeleton::skeleton() : timestamp(0),id(0), valid(false)
{
}

skeleton::skeleton(const skeleton& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;
    valid = s.valid;
}

skeleton::~skeleton()
{
    
}

skeleton::skeleton(skeleton&& s) :
    timestamp(s.timestamp), id(s.id), valid(s.valid),
    root(std::move(s.root))
{

}

skeleton& skeleton::operator=(const skeleton& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;
    valid = s.valid;

    return *this;
}

skeleton& skeleton::operator=(skeleton&& s)
{
    timestamp = s.timestamp;
    id = s.id;
    valid = s.valid;
    root = std::move(s.root);
    return *this;
}

bool skeleton::update_joint(joint_type jt, joint j)
{
    return root.update(jt,j);
}

joint skeleton::get_joint(joint_type jt)
{
    return root.deep_find(jt);
}

skeleton skeleton::default_skeleton()
{
    int center_y = 1100;
    int upper_body_y = 350;

    Eigen::Vector3d center_normal;
    center_normal << 0, 0, -100;

    skeleton s;

    // left arm
    joint left_shoulder = skeleton::create_arm(side::LEFT);

    // right arm
    joint right_shoulder = skeleton::create_arm(side::RIGHT);

    // left foot
    joint left_hip = skeleton::create_leg(side::LEFT);

    // right foot    
    joint right_hip = skeleton::create_leg(side::RIGHT);

    joint head = create_head();

    joint neck = joint::create_parent({head, right_shoulder,
                                        left_shoulder});
    neck.point << 0, upper_body_y, 0;
    neck.type = joint_type::NECK;
    neck.valid = true;

    joint center = joint::create_parent({neck, right_hip, left_hip});
    center.normal = center_normal;
    center.point << 0, center_y, 0;
    center.type = joint_type::CENTER;
    center.valid = true;
    s.root = center;

    return std::move(s);
}

joint skeleton::create_arm(side s)
{
    int hand_length = 50;
    int forearm_x = 50;
    int forearm_y = 355;
    int arm_x = 75;
    int arm_y = 320;
    int shoulder_x = 220;

    Eigen::Vector3d hand_normal;
    hand_normal << 100, 0, 0;

    joint shoulder;
    joint elbow;
    joint wrist;
    joint hand;
    if(s == side::LEFT) {
        shoulder.type = joint_type::SHOULDER_LEFT;
        elbow.type = joint_type::ELBOW_LEFT;
        wrist.type = joint_type::WRIST_LEFT;
        hand.type = joint_type::HAND_LEFT;
    } else {
        shoulder.type = joint_type::SHOULDER_RIGHT;
        elbow.type = joint_type::ELBOW_RIGHT;
        wrist.type = joint_type::WRIST_RIGHT;
        hand.type = joint_type::HAND_RIGHT;
    }


    hand.normal = (-1 * s) * hand_normal;
    hand.point << 0, -hand_length, 0;
    hand.valid = true;

    wrist.point << s * forearm_x, -forearm_y, 0;
    wrist.add_child(std::move(hand));
    wrist.valid = true;

    // elbows relative to shoulders
    elbow.point << (s * arm_x), -arm_y, 0;
    elbow.valid = true;
    elbow.add_child(std::move(wrist));

    // shoulders relative to neck
    shoulder.point << (s * shoulder_x), 0, 0;
    shoulder.valid = true;
    shoulder.add_child(std::move(elbow));

    return std::move(shoulder);
}

joint skeleton::create_leg(side s)
{
    int foot_length = 255;
    int lower_leg_y = 410;
    int thigh_y = 540;
    int hip_x = 180;
    int hip_y = 100;

    Eigen::Vector3d foot_normal;
    foot_normal << 0, 0, -100;

    joint foot;
    joint ankle;
    joint knee;
    joint hip;

    if(s == side::LEFT) {
        foot.type = joint_type::FOOT_LEFT;
        ankle.type = joint_type::ANKLE_LEFT;
        knee.type = joint_type::KNEE_LEFT;
        hip.type = joint_type::HIP_LEFT;
    } else {
        foot.type = joint_type::FOOT_RIGHT;
        ankle.type = joint_type::ANKLE_RIGHT;
        knee.type = joint_type::KNEE_RIGHT;
        hip.type = joint_type::HIP_RIGHT;
    }

    foot.normal = foot_normal;
    foot.point << 0, 0, -foot_length;
    foot.valid = true;

    ankle.point << 0, -lower_leg_y, 0;
    knee.point << 0, -thigh_y, 0;
    hip.point << s * hip_x, -hip_y, 0;

    ankle.add_child(std::move(foot));
    ankle.valid = true;
    knee.add_child(std::move(ankle));
    knee.valid = true;
    hip.add_child(std::move(knee));
    hip.valid = true;

    return std::move(hip);
}

joint skeleton::create_head()
{
    int head_y = 180;
    Eigen::Vector3d head_normal;
    head_normal << 0, 0, -100;

    joint head;
    head.normal = head_normal;
    head.point << 0, head_y, 0;
    head.type = joint_type::HEAD;
    head.valid = true;

    return std::move(head);
}

bool skeleton::equals(skeleton &s)
{
    return valid == s.valid && root.equals(s.root);
}

bool skeleton::operator==(skeleton &rhs)
{
    return equals(rhs);
}

}

ostream& operator<<(ostream& ost, const trame::skeleton &s)
{
    ost << "skeleton: timestamp - " << s.timestamp << " | id - " << s.id <<
           " | root - " << s.root;
    return ost;
}
