#include "trame.hpp"
#include "serialization/json_serializer.hpp"
#include "serialization/protobuf_serializer.hpp"

namespace trame {

trame::trame() : default_output(output_type::PROTOBUF)
{
    set_output(default_output);
}

trame::trame(const trame& t)
{
    
}

trame::~trame()
{
    
}

skeleton trame::get_skeleton()
{
    return skeleton_generator.get_next();
}

void trame::set_output(output_type ot)
{
    // try to create a new one
	switch(ot) {
        case output_type::JSON:
        serial = json_serializer();
			break;

        case output_type::PROTOBUF:        
        //serial = protobuf_serializer();
			break;

        case output_type::BOOST_SERIALIZE:
			break;
	}
}

std::vector<unsigned char> trame::get_serialized_skeleton()
{
    skeleton sk = skeleton_generator.get_next();
    return serial.serialize(sk);
}

}
