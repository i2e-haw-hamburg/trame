#include "trame.hpp"

namespace trame {

trame::trame() {
    
    
}

trame::trame(const trame& t) {
    
}

trame::~trame() {
    
}

std::vector<unsigned char> trame::get_skeleton() {
	// TODO: implement
    std::vector<unsigned char> v;
    v.push_back('1');
    return v;
}


void trame::set_output(output_type ot) {
	// TODO: implement
	switch(ot) {
        case output_type::JSON:
			break;

        case output_type::PROTOBUF:
			break;

        case output_type::BOOST_SERIALIZE:
			break;
	}
}
}
