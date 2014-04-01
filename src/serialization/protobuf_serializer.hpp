#ifndef TRAME_PROTOBUF_SERIALIZER_HPP
#define TRAME_PROTOBUF_SERIALIZER_HPP

#include "serializer.hpp"

namespace trame {

class protobuf_serializer : serializer
{
protected:
    protobuf_serializer();
    protobuf_serializer(const protobuf_serializer&);
    ~protobuf_serializer();

public:
    virtual vector<unsigned char> serialize(skeleton);
    virtual output_type get_type();
};

} // namespace trame

#endif
