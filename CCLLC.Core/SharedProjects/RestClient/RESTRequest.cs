using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;

    [DataContract]
    public abstract class RESTRequest<T> :  IRESTRequest<T> where T : class, IRESTResponse 
    {       
        protected IRESTEndpointConfiguration Configuration { get; }        

        protected RESTRequest(IAPIEndpoint endpoint, string accessToken = null) 
            : this(new RESTEndpointConfiguration(endpoint, accessToken))
        {            
        }

        protected RESTRequest(IRESTEndpointConfiguration configuration)
        {
            this.Configuration = configuration ?? throw new ArgumentNullException("restEndpoint cannot be null.");            
        }

        public virtual IAPIEndpoint ApiEndpoint
        {
            get
            {
                return Configuration.Endpoint;                                      
            }           
        }

        public virtual string AccessToken
        {
            get
            {
                return Configuration.AccessToken; 
            }
        }

        protected virtual void SetRequestHeaders(IWebRequest webRequest)
        {
            webRequest.Headers.Add("Cache-Control", "no-cache");
        }

        public abstract T Execute(IWebRequest webRequest);        
       
    }
}
