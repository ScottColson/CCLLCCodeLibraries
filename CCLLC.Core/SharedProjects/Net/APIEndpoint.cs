using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace CCLLC.Core.Net
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
            {                
                return new APIEndpoint(this.AbsoluteUri + string.Format("?{0}={1}", key, value));
            }

            return new APIEndpoint(this.AbsoluteUri + "?" + this.Query + string.Format("&{0}={1}", key, value));            
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

      
        public IReadOnlyDictionary<string, string> QueryParameters
        {
            get
            {
                var dictionary = new Dictionary<string, string>();
                var qry = this.Query;

                if (string.IsNullOrEmpty(qry)) return dictionary;


                foreach (var pair in qry.Split('&').Select(p => p.Split('=')).Select(p => new { key = p[0], value = p.Length == 1 ? p[1] : null }))
                {
                    dictionary.Add(pair.key, pair.value);
                }

                return dictionary;
            }
        }

       
        public Uri ToUri()
        {
            return this;
        }
    }
}
