using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AForge.Math;
using NetworkMessages.Trame;
using Trame;
using Trame.Implementation.Skeleton;


namespace TrameSerialization.Serializer
{
    public class ProtobufSerializer : ISerializer
    {
        public OutputType OutputType
        {
            get { return OutputType.JSON; }
        }

        public Stream Serialize(Trame.ISkeleton s)
        {
            SkeletonMessage message = ToMessage(s);
            var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, message);

            return stream;
        }

        public ISkeleton Deserialize(Stream stream)
        {
            var message = ProtoBuf.Serializer.Deserialize<SkeletonMessage>(stream);
            return FromMessage(message);
        }


        public ISkeleton FromMessage(SkeletonMessage message)
        {
            var skeleton = new Skeleton((uint)message.id, message.valid, (uint)message.timestamp);
            skeleton.Root = FromMessage(message.root);

            return skeleton;
        }

        public SkeletonMessage ToMessage(ISkeleton skeleton)
        {
            var message = new SkeletonMessage();
            message.id = skeleton.ID;
            message.timestamp = skeleton.Timestamp;
            message.valid = skeleton.Valid;
            message.root = ToMessage(skeleton.Root);

            return message;
        }

        private SkeletonMessage.Joint ToMessage(IJoint j)
        {
            if (j == null)
            {
                return null;
            }
            var joint = new SkeletonMessage.Joint { valid = j.Valid};
            var o = (Convert.ChangeType(j.JointType, TypeCode.Int32));
            if (o != null)
            {
                joint.type = (int)o;
            }
            joint.normal.AddRange(j.Normal.ToArray());
            joint.point.AddRange(j.Point.ToArray());

            joint.children.AddRange(j.GetChildren().Select(ToMessage));

            return joint;
        }

        public IJoint FromMessage(SkeletonMessage.Joint j)
        {
            var joint = new Joint((JointType)j.type, j.valid);
            joint.Normal = ListToVector(j.normal);
            joint.Point = ListToVector(j.point);

            j.children.ForEach(child => joint.AddChild(FromMessage(child)));

            return joint;
        }

        private static Vector3 ListToVector(List<float> l)
        {
            if (l.Count >= 3)
            {
                return new Vector3(l[0], l[1], l[2]);
            }
            return new Vector3(0);
        }
    }
}
