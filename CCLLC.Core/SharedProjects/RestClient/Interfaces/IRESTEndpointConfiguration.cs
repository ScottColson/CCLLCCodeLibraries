using System;

namespace CCLLC.Core.RESTClient
{ 
    public interface IRESTEndpointConfiguration
    {
        IAPIEndpoint Endpoint { get; }        
        string AccessToken { get; }
    }
}