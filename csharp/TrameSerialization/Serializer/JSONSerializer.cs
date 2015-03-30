using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Linq;
using System.Text;
using AForge.Math;
using Trame;


namespace TrameSerialization.Serializer
{
    class JSONSerializer : ISerializer
    {
        public OutputType OutputType
        {
            get { return OutputType.JSON; }
        }

        public Stream Serialize(Trame.ISkeleton<Vector4, Vector3> s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            var jsonSkeleton = new JsonObject();

            jsonSkeleton["id"] = s.ID;
            jsonSkeleton["timestamp"] = s.Timestamp;
            jsonSkeleton["valid"] = s.Valid;
            jsonSkeleton["root"] = JointToObject(s.Root);

            writer.Write(jsonSkeleton.ToString());
            writer.Flush();
            stream.Position = 0;
            return stream;;
        }

        public ISkeleton<Vector4, Vector3> Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }

        private static JsonValue JointToObject(IJoint<Vector4, Vector3> joint)
        {
            if (joint == null)
            {
                return null;
            }
            var jsonJoint = new JsonObject();
            jsonJoint["type"] = (int) (Convert.ChangeType(joint.JointType, TypeCode.Int32));
            jsonJoint["orientation"] = Vector4ToArray(joint.Orientation);
            jsonJoint["point"] = Vector3ToArray(joint.Point);
            jsonJoint["valid"] = joint.Valid;
            var children = new JsonArray();

            foreach (var child in joint.GetChildren())
            {
                children.Add(JointToObject(child));
            }

            jsonJoint["children"] = children;

            return jsonJoint;
        }

        private static JsonArray Vector3ToArray(Vector3 vector)
        {
            if (vector.Norm > 0)
            {
                var arr = new JsonArray {vector.X, vector.Y, vector.Z};
                return arr;
            }
            else
            {
                return null;
            }
        }

        private static JsonArray Vector4ToArray(Vector4 vector)
        {
            if (vector.Norm > 0)
            {
                var arr = new JsonArray {vector.X, vector.Y, vector.Z, vector.W};
                return arr;
            }
            else
            {
                return null;
            }
        }
    }
}
