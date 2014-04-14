#ifndef TRAME_JSON_SERIALIZER_HPP
#define TRAME_JSON_SERIALIZER_HPP

#include "serializer.hpp"
#include <json/json.h>

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

private:
    Json::FastWriter writer;
    Json::Value object_from_joint(joint);
    Json::Value array_from_vector(Eigen::Vector3d);
};

} // namespace trame

#endif
