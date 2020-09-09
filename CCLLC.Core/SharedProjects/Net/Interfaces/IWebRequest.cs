using System;
using System.Net;

namespace CCLLC.Core.Net
{
    public interface IWebRequest : IDisposable
    {   
        ICredentials Credentials { get; set; }
        WebHeaderCollection Headers { get; }
        TimeSpan Timeout { get; set; }
        IWebResponse Get();
        IWebResponse Delete();
        IWebResponse Post(byte[] data, string contentType = null, string contentEncoding = null);
        IWebResponse Put(string body, string contentType = null);
    }

    
    
}
