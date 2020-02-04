using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public class CDSExecutionContextFactory : ICDSExecutionContextFactory<ICDSPluginExecutionContext>
    {             
        public ICDSPluginExecutionContext CreateProcessContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container)
        {
            return new CDSPluginExecutionContext(serviceProvider, container, executionContext as IPluginExecutionContext);
        }
    }
}
