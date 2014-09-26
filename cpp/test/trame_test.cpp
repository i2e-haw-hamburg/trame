#include <iostream>
#include <cstdlib>
#include "trame.hpp"
#include <sys/time.h>
#include <ctime>
#include <vector>
#include <sys/resource.h>

using namespace std;

void test_performance(int time, bool hold) {
    timeval startTime;
    timeval runTime;
    long diff = 0;
    trame::trame t;    
    gettimeofday(&startTime, NULL);
    int count = 0;
    int valid_count = 0;
    vector<trame::skeleton> skeletons;
    trame::skeleton s;
    int who = RUSAGE_SELF;
    struct rusage usage;


    long printed = 0;
    cout << "sec;count;valid;memory" << endl;

    do {
        s = t.get_skeleton();
        if (hold) {
            skeletons.push_back(s);

        }
        if(s.valid) {
            valid_count++;
        }

        gettimeofday(&runTime, NULL);
        count++;
        diff = ((runTime.tv_sec * 1000) + (runTime.tv_usec / 1000))
            - ((startTime.tv_sec * 1000) + (startTime.tv_usec / 1000));
        diff = (int)(diff / 1000);

        if(printed < diff) {            
            getrusage(who,&usage);
            cout << diff << ";" << count << ";" << valid_count << ";" << usage.ru_maxrss << endl;
            printed = diff;
        }
    } while(diff <= time);

    cout << diff << ";" << count << ";" << 0 << endl;
}

int main(int argc, char* argv[])
{
    int time = 100;
    bool hold = true;

    if(argc >= 2) {
        time = atoi(argv[1]);
    }

    if(argc >= 3) {
        hold = true;
    }

    test_performance(time, hold);

    return 0;
}

