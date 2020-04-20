using System;
using System.Collections.Generic;

namespace CCLLC.Core.Net
{
    public interface IAPIEndpoint
    {
        IAPIEndpoint AddRoute(string path);

        IAPIEndpoint AddQuery(string key,string value);

        IReadOnlyDictionary<string,string> QueryParameters { get; }

        Uri ToUri();
    }
}
 