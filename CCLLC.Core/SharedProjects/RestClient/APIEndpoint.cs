using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    [Serializable()]
    public class APIEndpoint :  Uri, IAPIEndpoint
    {
        public APIEndpoint(string uriString) : base(uriString)
        {
        }

        protected APIEndpoint(SerializationInfo info, StreamingContext context) : base(info, context) 
        { }

        public IAPIEndpoint AddQuery(string key, string value)
        {
            if (string.IsNullOrEmpty(this.Query))
                return new APIEndpoint(this.AbsolutePath + string.Format("?{0}={1}", key, value));

            return new APIEndpoint(this.AbsolutePath + "?" + this.Query + string.Format("&{0}={1}", key, value));            
        }

        public IAPIEndpoint AddRoute(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (this.AbsolutePath.Length > 1)
                    return new APIEndpoint(this.AbsoluteUri + "/" + path + this.Query);

                return new APIEndpoint(this.AbsoluteUri + path + this.Query);
            }

            return this;
        }

        public Uri ToUri()
        {
            return this;
        }
    }
}
