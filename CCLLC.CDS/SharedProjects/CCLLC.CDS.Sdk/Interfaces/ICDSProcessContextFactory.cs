using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public interface ICDSProcessContextFactory<T> where T :  ICDSProcessContext
    {
        T CreateProcessContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container);
    }
}
