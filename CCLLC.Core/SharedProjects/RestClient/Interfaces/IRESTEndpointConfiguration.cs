using System;

namespace CCLLC.Core.RESTClient
{ 
    public interface IRESTEndpointConfiguration
    {
        Uri Endpoint { get; }        
        string AccessToken { get; }
    }
}