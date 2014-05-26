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
     * @brief Returns the skeleton in a defined serialized format.
     *
     * This method returns the serialized skeleton in the setted format. At the
     * current stage two formats are intended. The first is a json serialization
     * and the second is a serialization based on Google's protocol buffer.
     * The returned skeleton is the same as in get_skeleton().
     *
     * @return a serialized skeleton
     */
    std::vector<unsigned char> get_serialized_skeleton();
    /**
     * @brief Sets the output type for the get_serialized_skeleton() method.
     * @param ot the desired output type
     */
    void set_output(output_type ot);
    /**
     * @brief Returns the current skeleton from a tracking device.
     *
     * The method uses the configured device and don't serialize the fetched
     * result.
     *
     * @return the current skeleton
     */
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
