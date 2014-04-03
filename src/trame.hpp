#ifndef TRAME_TRAME_HPP
#define TRAME_TRAME_HPP

#include "skeleton/skeleton.hpp"
#include "serialization/serializer.hpp"
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
    /**
     * @brief trame
     */
    trame();
    /**
     * @brief trame
     */
    trame(const trame&);
    /**
     *
     */
    ~trame();
    /**
     * @brief get_skeleton
     * @return
     */
    std::vector<unsigned char> get_skeleton();
    /**
     * @brief
     * @param ot
     */
    void set_output(output_type);

private:
    /**
     * @brief default_output
     */
    output_type default_output;
    /**
     * @brief serial
     */
    serializer* serial;
    generator skeleton_generator;
};

} // namespace trame

#endif
