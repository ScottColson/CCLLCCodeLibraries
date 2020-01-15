using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Serialization;

    [DataContract]
    public abstract class SerializedRESTRequest :  RESTRequest
    {               
        protected IDataSerializer Serializer { get; }

        protected SerializedRESTRequest(IDataSerializer serializer, IRESTEndpointConfiguration configuration)
            :base(configuration)
        {
            
            this.Serializer = serializer ?? throw new ArgumentNullException("serializer cannot be null.");            
        }           
        
    }
}
