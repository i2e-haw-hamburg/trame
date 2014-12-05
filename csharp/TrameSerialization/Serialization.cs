using Trame;
using TrameSerialization.Serializer;

namespace TrameSerialization
{
    public class Serialization
    {
        private OutputType ot;
        private ISerializer serializer;

        public Serialization(OutputType ot)
        {
            this.OutputType = ot;
        }

        public Serialization() : this(OutputType.JSON) {}

        public OutputType OutputType
        {
            set { 
                this.ot = value;
                SetSerializer(); 
            }
        }

        private void SetSerializer()
        {
            switch (ot)
            {
                case OutputType.JSON:
                    serializer = new JSONSerializer();
                    break;
                
            }
        }

        public string Serialize(ISkeleton skeleton)
        {
            return serializer.Serialize(skeleton);
        }
    }
}
