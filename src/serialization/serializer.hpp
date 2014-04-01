#ifndef TRAME_SERIALIZER_HPP
#define TRAME_SERIALIZER_HPP

#include <vector>
#include "../skeleton/skeleton.hpp"

using namespace std;

namespace trame {

enum class output_type {
    JSON,
    PROTOBUF,
    BOOST_SERIALIZE
};

class serializer
{
protected:
    serializer();
    serializer(const serializer&);
    ~serializer();

public:
    virtual vector<unsigned char> serialize(skeleton) = 0;
    virtual output_type get_type() = 0;
};

} // namespace trame

#endif
