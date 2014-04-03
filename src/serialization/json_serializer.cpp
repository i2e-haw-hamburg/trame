#include "json_serializer.hpp"
#include "libjson/libjson.h"
#include <iostream>

using namespace libjson;

namespace trame {

json_serializer::json_serializer() {

}

json_serializer::json_serializer(const json_serializer&) {

}

json_serializer::~json_serializer() {

}

std::vector<unsigned char> json_serializer::serialize(skeleton) {
    libjson::JSONNode node(JSON_NODE);
    node.push_back(JSONNode("String Node", "String Value"));
    node.push_back(JSONNode("Integer Node", 42));
    node.push_back(JSONNode("Floating Point Node", 3.14));
    node.push_back(JSONNode("Boolean Node", true));
    std::string jc = node.write_formatted();
    std::cout << jc << std::endl;
}

output_type json_serializer::get_type() {
    return output_type::JSON;
}

}
