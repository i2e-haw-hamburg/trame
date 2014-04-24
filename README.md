TRAME - TRAck ME (if you can)
=============================

Skeleton tracking abstraction library.

## Build

```BASH
# create a directory for the build, eg. 'build'
mkdir build
cd build
# call cmake
cmake ..
# call make
make
```

## Tests

In the test folder are a performance test and a skeleton viewer. The skeleton viewer is implemented in Python with pyprocessing. So you need to install this first, before you can use the viewer.

For more information visit: http://code.google.com/p/pyprocessing/wiki

Skeleton exportet with JSON 
```
{"id":0,"root":{"children":[{"children":[{"children":[],"normal":[0,0,10],"point":[0,180,0],"type":1},{"children":[{"children":[{"children":[{"children":[],"normal":[-10,0,0],"point":[50,475,0],"type":124}],"normal":null,"point":[-50,-455,0],"type":123}],"normal":null,"point":[75,-320,0],"type":122}],"normal":null,"point":[-220,0,0],"type":121},{"children":[{"children":[{"children":[{"children":[],"normal":[10,0,0],"point":[50,-475,0],"type":114}],"normal":null,"point":[50,-455,0],"type":113}],"normal":null,"point":[-75,-320,0],"type":112}],"normal":null,"point":[220,0,0],"type":111}],"normal":null,"point":[0,350,0],"type":5},{"children":[{"children":[{"children":[{"children":[],"normal":[0,0,-10],"point":[-20,0,-255],"type":224}],"normal":null,"point":[0,-410,0],"type":223}],"normal":null,"point":[0,-540,0],"type":222}],"normal":null,"point":[180,-100,0],"type":221},{"children":[{"children":[{"children":[{"children":[],"normal":[0,0,10],"point":[-20,0,-255],"type":214}],"normal":null,"point":[0,-410,0],"type":213}],"normal":null,"point":[0,-540,0],"type":212}],"normal":null,"point":[-180,-100,0],"type":211}],"normal":[0,0,10],"point":[0,1100,0],"type":9},"timestamp":4215765565}
```