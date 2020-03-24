using System;

namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;

    public interface IRESTRequest
    {
        IAPIEndpoint ApiEndpoint { get; }
        IRESTResponse Execute(IHttpWebRequest webRequest);
    }
}
