#include <iostream>
#include "trame.hpp"
#include <chrono>
#include <thread>

using namespace std;

int main(int argc, char* argv[])
{
    trame::trame t;

    while(!t.get_skeleton().valid) {
        std::this_thread::sleep_for(std::chrono::milliseconds(30));
    }

    for (auto& c : t.get_serialized_skeleton()) {
        cout << c;
    }

    return 0;
}

