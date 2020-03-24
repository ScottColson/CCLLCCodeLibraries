using System;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;

    public class CDSWebRequestFactory : ICDSWebRequestFactory
    {      
        public IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null)
        {
            return new HttpWebRequestWrapper(address);
        }
    }
}
