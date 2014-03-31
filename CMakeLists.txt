# CMake version 2.8 or above
cmake_minimum_required(VERSION 2.8)
project(trame)

# Include directories for header files
include_directories(.
            ${CMAKE_SOURCE_DIR}/src
            ${CMAKE_SOURCE_DIR}/test)

SET(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

# Set source files for trame library
set(TRAME_SRC
    src/trame.cpp
    src/skeleton/skeleton.cpp
    src/skeleton/joint.cpp)

# Set source files for trame test app
set(TRAME_TEST_SRC
    test/trame_test.cpp)

# Create library trame from sources
add_library(trame ${TRAME_SRC})

# Create test application
add_executable(trame-test ${TRAME_TEST_SRC})

# link library with executable
target_link_libraries(trame-test trame)
