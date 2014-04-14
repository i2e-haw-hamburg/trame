#include <iostream>
#include <cstdlib>
#include "trame.hpp"

using namespace std;

void test_performance(int times) {
    cout << "Trame - Test Performance" << endl
       << "========================" << endl << endl;

    cout << "Run " << times << " times!" << endl;

    trame::trame t;

    bool print = false;

    for(int i = 0; i < times; ++i) {
        t.get_skeleton();
        int percent = i * 100 / times;
        if(((int)(percent % 10)) == 0) {
            if(!print) {
                cout << percent << "% done" << endl;
                print = true;
            }
        } else {
            print = false;
        }
    }

    cout << "100% done" << endl << endl;
}

int main(int argc, char* argv[])
{

    int times = 100;

    if(argc >= 2) {
        times = atoi(argv[1]);
    }


    test_performance(times);

    return 0;
}

