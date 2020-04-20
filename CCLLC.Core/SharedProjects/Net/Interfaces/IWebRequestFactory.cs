using System;

namespace CCLLC.Core.Net
{
    public interface IWebRequestFactory
    {
        IWebRequest CreateWebRequest(Uri address, string telemetryTag = null);

        IWebRequest CreateWebRequest(IAPIEndpoint endpoint, string telemetryTag = null);
    }
}
