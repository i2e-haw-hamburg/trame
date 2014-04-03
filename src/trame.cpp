#include "trame.hpp"
#include "serialization/json_serializer.hpp"
#include "serialization/protobuf_serializer.hpp"

namespace trame {

trame::trame() : default_output(output_type::JSON)
{
    set_output(default_output);
}

trame::trame(const trame& t) {
    
}

trame::~trame() {
    
}

std::vector<unsigned char> trame::get_skeleton() {
    skeleton sk = skeleton_generator.get_next();
    return serial->serialize(sk);
}

void trame::set_output(output_type ot) {
    // remove the last serializer from memory
    if(serial) {
        delete serial;
    }
    // try to create a new one
	switch(ot) {
        case output_type::JSON:
        serial = new json_serializer();
			break;

        case output_type::PROTOBUF:        
        serial = new protobuf_serializer();
			break;

        case output_type::BOOST_SERIALIZE:
			break;
	}
}
}
