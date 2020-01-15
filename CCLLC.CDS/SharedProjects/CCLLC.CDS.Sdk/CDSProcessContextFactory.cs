using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public class CDSProcessContextFactory : ICDSProcessContextFactory<ICDSPluginProcessContext>
    {             
        public ICDSPluginProcessContext CreateProcessContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container)
        {
            return new CDSPluginProcessContext(serviceProvider, container, executionContext as IPluginExecutionContext);
        }
    }
}
