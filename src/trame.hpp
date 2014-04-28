#ifndef TRAME_TRAME_HPP
#define TRAME_TRAME_HPP

#include "skeleton/skeleton.hpp"
#include "serialization/serializer.hpp"
#include "serialization/json_serializer.hpp"
#include "serialization/protobuf_serializer.hpp"
#include "device/generator.hpp"
#include <vector>

namespace trame {

enum class output_type;
/**
 * @brief The trame class
 */
class trame
{
public:
    trame();
    trame(const trame&);
    ~trame();
    /**
     * @brief get_serialized_skeleton
     * @return
     */
    std::vector<unsigned char> get_serialized_skeleton();
    /**
     * @brief
     * @param ot
     */
    void set_output(output_type);

    skeleton get_skeleton();
private:
    /**
     * @brief default_output
     */
    output_type default_output;
    /**
     * @brief serial
     */
    json_serializer serial;
    generator skeleton_generator;
};

} // namespace trame

#endif
