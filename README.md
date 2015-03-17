TRAME - TRAck ME (if you can)
=============================

Skeleton tracking abstraction library.

## Tracking Devices

The currently supported devices for tracking user skeleton data are listed below:

 * Microsoft Kinect XBOX 360
 * Microsoft Kinect for Windows
 * LEAP Motion

There are different SDKs available so it is possible, that you must write a new `adapter` for this specific `device`. 

## Skeletons

The most significant part of **Trame** is the uniform `skeleton` model.


## Changes and Roadmap

 * `04/2014`: First implementation in *C++*
 * `09/2014`: Start with *C#* implementation
 * `12/2014`: *C++* implementation is deprecated
 * `03/2015`: Separation of skeleton library for better integration into other projects
 * `04/2015`: Operations on skeletons like `diff`, `mean` and `angle` are supported

## Licence

This software is licensed under the [Apache License v2.0](http://www.apache.org/licenses/LICENSE-2.0).