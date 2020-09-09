using CCLLC.Core.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SamplePlugin.Model
{
    [DataContract]
    public class Registrations : List<RegistrationData>, ISerializableData
    {
        public string ToString(IDataSerializer serializer)
        {
            return serializer.Serialize(this);
        }
    }

    [DataContract]
    public class RegistrationData : ISerializableData
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public DateTime CreatedOn { get; set; }

        public string ToString(IDataSerializer serializer)
        {
            return serializer.Serialize(this);
        }
    }
}
