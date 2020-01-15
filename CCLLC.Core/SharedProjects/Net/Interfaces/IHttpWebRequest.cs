using System;
using System.Net;

namespace CCLLC.Core.Net
{
    public interface IHttpWebRequest
    {   
        ICredentials Credentials { get; set; }
        WebHeaderCollection Headers { get; }
        TimeSpan Timeout { get; set; }
        IHttpWebResponse Get();
        IHttpWebResponse Post(byte[] data, string contentType = null, string contentEncoding = null);
        IHttpWebResponse Put(string body, string contentType = null);
    }

    
    
}
