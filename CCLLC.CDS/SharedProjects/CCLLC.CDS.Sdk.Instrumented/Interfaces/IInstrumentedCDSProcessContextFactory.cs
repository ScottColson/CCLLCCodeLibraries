using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Telemetry;

    public interface IInstrumentedCDSProcessContextFactory<T> where T :  IInstrumentedCDSProcessContext
    {
        T CreateProcessContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, IComponentTelemetryClient telemetryClient );
    }
}
