#ifndef TRAME_TRAME_HPP
#define TRAME_TRAME_HPP

#include "skeleton/skeleton.hpp"
#include "serialization/serializer.hpp"
#include <vector>

namespace trame {

enum class output_type;

class trame
{
public:
    trame();
    trame(const trame&);
    ~trame();

    std::vector<unsigned char> get_skeleton();
    void set_output(output_type);

private:
    output_type default_output = output_type::JSON;
};

} // namespace trame

#endif
