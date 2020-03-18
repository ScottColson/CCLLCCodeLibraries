using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public class CDSExecutionContextFactory : ICDSExecutionContextFactory<ICDSPluginExecutionContext>
    {             
        public ICDSPluginExecutionContext CreateCDSExecutionContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, eRunAs runAs = eRunAs.User)
        {
            return new CDSPluginExecutionContext(serviceProvider, container, executionContext as IPluginExecutionContext, runAs);
        }
    }
}
