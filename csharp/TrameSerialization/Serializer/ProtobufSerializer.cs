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

        public Stream Serialize(Trame.ISkeleton<Vector4, Vector3> s)
        {
            SkeletonMessage message = ToMessage(s);
            var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, message);

            return stream;
        }

        public ISkeleton<Vector4, Vector3> Deserialize(Stream stream)
        {
            var message = ProtoBuf.Serializer.Deserialize<SkeletonMessage>(stream);
            return FromMessage(message);
        }


        public ISkeleton<Vector4, Vector3> FromMessage(SkeletonMessage message)
        {
            var skeleton = new Skeleton<Vector4, Vector3>((uint)message.id, message.valid, (uint)message.timestamp);
            skeleton.Root = FromMessage(message.root);

            return skeleton;
        }

        public SkeletonMessage ToMessage(ISkeleton<Vector4, Vector3> skeleton)
        {
            var message = new SkeletonMessage();
            message.id = skeleton.ID;
            message.timestamp = skeleton.Timestamp;
            message.valid = skeleton.Valid;
            message.root = ToMessage(skeleton.Root);

            return message;
        }

        private SkeletonMessage.Joint ToMessage(IJoint<Vector4, Vector3> j)
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
            joint.orientation.AddRange(j.Orientation.ToArray());
            joint.point.AddRange(j.Point.ToArray());

            joint.children.AddRange(j.GetChildren().Select(ToMessage));

            return joint;
        }

        public IJoint<Vector4, Vector3> FromMessage(SkeletonMessage.Joint j)
        {
            var joint = new OrientedJoint<Vector4, Vector3>((JointType)j.type, j.valid);
            joint.Orientation = ListToVector4(j.orientation);
            joint.Point = ListToVector3(j.point);

            j.children.ForEach(child => joint.AddChild(FromMessage(child)));

            return joint;
        }

        private static Vector3 ListToVector3(List<float> l)
        {
            return l.Count >= 3 ? new Vector3(l[0], l[1], l[2]) : new Vector3(0);
        }

        private static Vector4 ListToVector4(List<float> l)
        {
            return l.Count >= 3 ? new Vector4(l[0], l[1], l[2], l[3]) : new Vector4(0);
        }
    }
}
