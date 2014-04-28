#include <iostream>
#include "trame.hpp"

using namespace std;

int main(int argc, char* argv[])
{
    trame::trame t;

    for (auto& c : t.get_serialized_skeleton()) {
        cout << c;
    }

    return 0;
}

