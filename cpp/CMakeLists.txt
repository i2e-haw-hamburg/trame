# CMake version 2.8 or above
cmake_minimum_required(VERSION 2.8)
project(trame)

# Include directories for header files
include_directories(.
            ${CMAKE_SOURCE_DIR}/src
            ${CMAKE_SOURCE_DIR}/test)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(TRAME_LIBRARY trame)
set(TEST_DIR test)

# Set source files for trame library
set(TRAME_SRC
    src/trame.cpp
    src/skeleton/skeleton.cpp
    src/skeleton/joint.cpp
    src/device/generator.cpp
    src/device/device_abstraction.cpp
    src/device/device_stub.cpp
    src/device/skeleton_mockup.cpp
    src/device/kinect_sdk.cpp
    src/device/leap_motion.cpp
    src/device/nite.cpp
    src/device/adapter/leap_adapter.cpp
    src/device/adapter/nite_adapter.cpp
    src/serialization/serializer.cpp
    src/serialization/protobuf_serializer.cpp
    src/serialization/json_serializer.cpp)

# Set source files for trame test app
set(TRAME_TEST_SRC
    ${TEST_DIR}/trame_test.cpp)
# Set source files for trame viewer app
set(TRAME_VIEWER_SRC
    ${TEST_DIR}/trame_viewer.cpp)

# Create library trame from sources
add_library(trame ${TRAME_SRC})

find_package( jsoncpp )
include_directories( ${jsoncpp_INCLUDE_DIRS} )

find_package( leap )
include_directories( ${leap_INCLUDE_DIRS} )

target_link_libraries( ${TRAME_LIBRARY} ${jsoncpp_LIBRARIES} )
target_link_libraries( ${TRAME_LIBRARY} ${leap_LIBRARIES} )

# Create test application
add_executable(trame-test ${TRAME_TEST_SRC})

# Create viewer application
add_executable(trame-viewer ${TRAME_VIEWER_SRC})

# link library with executable
target_link_libraries(trame-test ${TRAME_LIBRARY})
target_link_libraries(trame-viewer ${TRAME_LIBRARY})

# test stuff
macro(add_unit_test name)
  add_executable(test_${name} ${TEST_DIR}/test_${name}.cpp)
  target_link_libraries(test_${name} ${TRAME_LIBRARY})
  add_test(${name} test_${name})
endmacro()

enable_testing()

add_unit_test(skeleton)
