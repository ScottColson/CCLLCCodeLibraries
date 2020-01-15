using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Telemetry;

    public class InstrumentedCDSProcessContextFactory : IInstrumentedCDSProcessContextFactory<IInstrumentedCDSPluginProcessContext>
    {             
       

        public IInstrumentedCDSPluginProcessContext CreateProcessContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, IComponentTelemetryClient telemetryClient)
        {
            return new InstrumentedCDSPluginProcessContext(serviceProvider, container, executionContext as IPluginExecutionContext, telemetryClient);
        }
    }
}
