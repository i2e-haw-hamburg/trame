using System;
using System.Collections.Generic;
using Trame;
using Trame.Implementation.Skeleton;

namespace TrameSkeleton.Implementation
{
    public class MessageDeserializer<K, T, SM, JM> where T : new() where K : new()
    {
        /**
        public static ISkeleton<K, T> Deserialize(Func<IList<float>, K> listToVector4, Func<IList<float>, T> listToVector3, SM message)
        {
            var skeleton = new Skeleton<K, T>((uint)message.id, message.valid, (uint)message.timestamp)
            {
                Root = FromMessage(listToVector4, listToVector3, message.root)
            };

            return skeleton;
        }

        private static IJoint<K, T> FromMessage(Func<IList<float>, K> listToVector4, Func<IList<float>, T> listToVector3, Func<JointType, bool, OrientedJoint<K, T>> constructJoint)
        {
            var joint = new OrientedJoint<K, T>((JointType)j.type, j.valid)
            {
                Orientation = listToVector4(j.orientation),
                Point = listToVector3(j.point)
            };

            j.children.ForEach(child => joint.AddChild(FromMessage(listToVector4, listToVector3, child)));

            return joint;
        }
        */
    }
}
