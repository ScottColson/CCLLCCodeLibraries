using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public interface ICDSExecutionContextFactory<T> where T :  ICDSExecutionContext
    {
        T CreateProcessContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, eRunAs runAs = eRunAs.User);
    }
}
