using System;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;

    public interface ICDSWebRequestFactory
    {
        IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null);
    }
}
