#ifndef TRAME_DEVICE_ABSTRACTION_HPP
#define TRAME_DEVICE_ABSTRACTION_HPP

#include "../skeleton/skeleton.hpp"

namespace trame {

class device_abstraction
{
public:
    /**
     * @brief get
     * @return
     */
    virtual skeleton get() = 0;
protected:
    /**
     * @brief
     *
     * @return the time of the day in milliseconds
     */
    int current_time();
};

} // namespace trame

#endif
