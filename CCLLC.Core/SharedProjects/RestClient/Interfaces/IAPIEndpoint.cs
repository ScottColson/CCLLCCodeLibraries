using System;
using System.Runtime.Serialization;

namespace CCLLC.Core.RESTClient
{
    public interface IAPIEndpoint 
    {
        IAPIEndpoint AddRoute(string path);

        IAPIEndpoint AddQuery(string key,string value);

        Uri ToUri();
    }
}
 