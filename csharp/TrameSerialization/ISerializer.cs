using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AForge.Math;
using Trame;

namespace TrameSerialization
{
    public interface ISerializer
    {
        OutputType OutputType { get; }
        Stream Serialize(ISkeleton<Vector4, Vector3> s);
        ISkeleton<Vector4, Vector3> Deserialize(Stream stream);
    }

    public enum OutputType
    {
        JSON, PROTOBUF, BASIC
    }
}
