using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Telemetry;

    public interface IInstrumentedCDSExecutionContextFactory<T> where T :  IInstrumentedCDSExecutionContext
    {
        T CreateCDSExecutionContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, IComponentTelemetryClient telemetryClient );
    }
}
