namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;

    public class RESTEndpointConfiguration : IRESTEndpointConfiguration
    {
        public IAPIEndpoint Endpoint { get; }

        public string AccessToken { get; }

        public RESTEndpointConfiguration(IAPIEndpoint endpoint, string accessToken = null)
        {
            this.Endpoint = endpoint;
            this.AccessToken = accessToken;
        }

    }
}
