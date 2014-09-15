#include <iostream>
#include <cstdlib>
#include "skeleton/skeleton.hpp"

using namespace std;
using namespace trame;

int main(int argc, char* argv[])
{
    skeleton s = skeleton::default_skeleton();
    joint head;
    head.type = joint_type::HEAD;
    head.point << 1, 2, 3;
    s.update_joint(joint_type::HEAD, head);

    joint head2 = s.get_joint(joint_type::HEAD);

    cout << s.get_joint(joint_type::NECK) << endl;

    return head == head2 ? 0 : -1;
}

