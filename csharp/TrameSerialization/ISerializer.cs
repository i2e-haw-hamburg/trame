using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Trame;

namespace TrameSerialization
{
    public interface ISerializer
    {
        OutputType OutputType { get; }
        Stream Serialize(ISkeleton s);
        ISkeleton Deserialize(Stream stream);
    }

    public enum OutputType
    {
        JSON, PROTOBUF, BASIC
    }
}
