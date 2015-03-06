using System.IO;
using Trame;
using TrameSerialization.Serializer;

namespace TrameSerialization
{
    public class Serialization
    {
        private OutputType ot;
        private ISerializer serializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ot"></param>
        public Serialization(OutputType ot)
        {
            this.OutputType = ot;
        }
        /// <summary>
        /// 
        /// </summary>
        public Serialization() : this(OutputType.JSON) {}

        /// <summary>
        /// 
        /// </summary>
        public OutputType OutputType
        {
            set { 
                this.ot = value;
                SetSerializer(); 
            }
            get { return this.ot; }
        }

        private void SetSerializer()
        {
            switch (ot)
            {
                case OutputType.JSON:
                    serializer = new JSONSerializer();
                    break;
                case OutputType.BASIC:
                    serializer = new BasicSerializer();
                    break;
                
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="skeleton"></param>
        /// <returns></returns>
        public Stream Serialize(ISkeleton skeleton)
        {
            return serializer.Serialize(skeleton);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public ISkeleton Deserialize(Stream stream)
        {
            return serializer.Deserialize(stream);
        }
    }
}
