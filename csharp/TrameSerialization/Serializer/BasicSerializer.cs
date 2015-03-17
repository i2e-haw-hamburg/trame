using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Trame;
using Trame.Implementation.Skeleton;

namespace TrameSerialization.Serializer
{
    /// <summary>
    /// 
    /// </summary>
    class BasicSerializer : ISerializer
    {
        readonly IFormatter formatter = new BinaryFormatter();

        public OutputType OutputType
        {
            get { return OutputType.BASIC; }
        }

        public Stream Serialize(ISkeleton s)
        {
            var stream = new MemoryStream();
            formatter.Serialize(stream, s);
            stream.Position = 0;
            return stream;
        }

        public ISkeleton Deserialize(Stream stream)
        {
            return (Skeleton)formatter.Deserialize(stream);
        }
    }
}
