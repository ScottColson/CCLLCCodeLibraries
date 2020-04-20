using System;

namespace CCLLC.Core.Net
{
    public class WebRequestFactory : IWebRequestFactory
    {
        public IWebRequest CreateWebRequest(Uri address, string telemetryTag = null)
        {
            return new HttpWebRequestWrapper(address);
        }

        public IWebRequest CreateWebRequest(IAPIEndpoint endpoint, string telemetryTag = null)
        {
            return new HttpWebRequestWrapper(endpoint);
        }
    }
}
