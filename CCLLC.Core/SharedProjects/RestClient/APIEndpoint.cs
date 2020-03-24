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

        public IAPIEndpoint AddRoute(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (this.AbsolutePath.Length > 1)
                    return new APIEndpoint(this.AbsoluteUri + "/" + path);

                return new APIEndpoint(this.AbsoluteUri + path);
            }

            return this;
        }

        public Uri ToUri()
        {
            return this;
        }
    }
}
