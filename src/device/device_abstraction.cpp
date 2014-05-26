#include "device_abstraction.hpp"
#include <sys/time.h>

namespace trame {


int device_abstraction::current_time()
{
    struct timeval time;
    gettimeofday(&time,NULL);
    return (time.tv_usec / 1000 + time.tv_sec * 1000) % (24 * 60 * 60 * 1000);
}

}
