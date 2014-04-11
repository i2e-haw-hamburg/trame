#include <iostream>
#include "trame.hpp"

using namespace std;

int main()
{
    cout << "Trame Test" << endl << endl;

    trame::trame t;

    for (auto& c : t.get_skeleton()) {
        cout << c;
    }

    cout << endl;

    return 0;
}

