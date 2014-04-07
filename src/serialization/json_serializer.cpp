#include "json_serializer.hpp"
#include <json/json.h>
#include <iostream>

namespace trame {

json_serializer::json_serializer() : writer(new Json::FastWriter) {

}

json_serializer::json_serializer(const json_serializer&) {

}

json_serializer::~json_serializer() {
    delete writer;
}

std::vector<unsigned char> json_serializer::serialize(skeleton s) {
    Json::Value node;

    node["timestamp"] = s.timestamp;
    node["id"] = s.id;
    node["root"] = object_from_joint(s.root);

    std::string serialized = writer->write(node);
    std::vector<unsigned char> bytes(serialized.begin(), serialized.end());

    return bytes;
}

output_type json_serializer::get_type() {
    return output_type::JSON;
}

Json::Value json_serializer::object_from_joint(joint j) {
    Json::Value joint_obj;
    joint_obj["type"] = (int)j.type;
    joint_obj["normal"] = array_from_vector(j.normal);
    joint_obj["point"] = array_from_vector(j.point);
    Json::Value children(Json::arrayValue);

    for (joint child : j.children) {
        children.append(object_from_joint(child));
    }

    joint_obj["children"] = children;
    return joint_obj;
}

Json::Value json_serializer::array_from_vector(Eigen::Vector3d v) {
    if(v.norm() > 0) {
        Json::Value vector_arr;
        vector_arr.append(v(0));
        vector_arr.append(v(1));
        vector_arr.append(v(2));

        return vector_arr;
    } else {
        return Json::Value(Json::nullValue);
    }
}

}
