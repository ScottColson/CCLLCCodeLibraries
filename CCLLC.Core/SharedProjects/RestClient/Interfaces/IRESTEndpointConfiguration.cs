namespace CCLLC.Core.RESTClient
{
    using CCLLC.Core.Net;

    public interface IRESTEndpointConfiguration
    {
        IAPIEndpoint Endpoint { get; }
        string AccessToken { get; }
    }
}