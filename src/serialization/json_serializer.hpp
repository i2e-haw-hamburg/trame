#ifndef TRAME_JSON_SERIALIZER_HPP
#define TRAME_JSON_SERIALIZER_HPP

#include "serializer.hpp"

namespace trame {

class json_serializer : public serializer
{
public:
    json_serializer();
    json_serializer(const json_serializer&);
    ~json_serializer();

public:
    virtual std::vector<unsigned char> serialize(skeleton);
    virtual output_type get_type();
};

} // namespace trame

#endif
