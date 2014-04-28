#include "skeleton.hpp"

namespace trame
{

skeleton::skeleton() : timestamp(0),id(0)
{
}

skeleton::skeleton(const skeleton& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;
}

skeleton::~skeleton()
{
    
}

skeleton::skeleton(skeleton&& s) :
    timestamp(s.timestamp), id(s.id),
    root(std::move(s.root))
{

}

skeleton& skeleton::operator=(const skeleton& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = s.root;

    return *this;
}

skeleton& skeleton::operator=(skeleton&& s)
{
    timestamp = s.timestamp;
    id = s.id;
    root = std::move(s.root);
    return *this;
}

}
