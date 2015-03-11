using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using AForge.Math;
using NetworkMessages.Trame;
using Trame;
using Trame.Implementation.Skeleton;
using Trame.Implementation.Skeleton;


namespace TrameSerialization.Serializer
{
    class ProtobufSerializer : ISerializer
    {
        public OutputType OutputType
        {
            get { return OutputType.JSON; }
        }

        public Stream Serialize(Trame.ISkeleton s)
        {
            SkeletonMessage message = s.ToMessage();
            var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, message);

            return stream;
        }

        public ISkeleton Deserialize(Stream stream)
        {
            var message = ProtoBuf.Serializer.Deserialize<SkeletonMessage>(stream);
            return Skeleton.FromMessage(message);
        }
    }
}
