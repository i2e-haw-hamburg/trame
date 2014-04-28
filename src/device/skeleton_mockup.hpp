#ifndef TRAME_SKELETON_MOCKUP_HPP
#define TRAME_SKELETON_MOCKUP_HPP

#include "device_abstraction.hpp"
#include "../skeleton/skeleton.hpp"

namespace trame {

class skeleton_mockup : public device_abstraction
{
public:
    skeleton_mockup();
    skeleton_mockup(const skeleton_mockup&);
    ~skeleton_mockup();
    /**
     * @brief Creates a skeleton with the parameters of an average male.
     *
     * The method can return the skeleton of a dwarf to, if you need one in your
     * application.
     *
     * @return a skeleton mockup
     */
    virtual skeleton get();

private:
    bool is_dwarf;
};

} // namespace trame

#endif
