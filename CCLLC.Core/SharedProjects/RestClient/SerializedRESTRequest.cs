using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;
    using CCLLC.Core.Serialization;

    [DataContract]
    public abstract class SerializedRESTRequest<T> :  RESTRequest<T> where T : class, IRESTResponse
    {              
        protected IDataSerializer Serializer { get; }

        protected SerializedRESTRequest(IDataSerializer serializer, IAPIEndpoint endpoint, string accessToken = null) : this(serializer, new RESTEndpointConfiguration(endpoint, accessToken))
        {
        }

        protected SerializedRESTRequest(IDataSerializer serializer, IRESTEndpointConfiguration configuration)
            :base(configuration)
        {            
            this.Serializer = serializer ?? throw new ArgumentNullException("serializer cannot be null.");            
        }           
        
    }
}
