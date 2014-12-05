using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trame;

namespace TrameSerialization
{
    public interface ISerializer
    {
        OutputType OutputType { get; }
        string Serialize(ISkeleton s);
    }

    public enum OutputType
    {
        JSON, PROTOBUF
    }
}
