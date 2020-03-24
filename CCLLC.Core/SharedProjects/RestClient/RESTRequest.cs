using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;

    [DataContract]
    public abstract class RESTRequest :  IRESTRequest 
    {       
        protected IRESTEndpointConfiguration Configuration { get; }        

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

        public virtual string AuthenticationToken
        {
            get
            {
                return Configuration.AccessToken; 
            }
        }

        protected virtual void SetRequestHeaders(IHttpWebRequest webRequest)
        {
            webRequest.Headers.Add("Cache-Control", "no-cache");
        }

        public abstract IRESTResponse Execute(IHttpWebRequest webRequest);
        
    }
}
