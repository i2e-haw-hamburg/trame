#include "skeleton.hpp"

using namespace trame;

skeleton::skeleton() : timestamp(0),id(0) {
}

skeleton::skeleton(const skeleton& s) {
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;
}

skeleton::~skeleton() {
    
}
