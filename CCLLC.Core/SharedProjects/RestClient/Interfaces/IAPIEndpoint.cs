using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    public interface IAPIEndpoint 
    {
        IAPIEndpoint AddRoute(string path);

        Uri ToUri();
    }
}
