using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Telemetry;

    /// <summary>
    /// Factory for generating <see cref="InstrumentedCDSPluginExecutionContext"/>.
    /// </summary>
    public class InstrumentedCDSExecutionContextFactory : IInstrumentedCDSExecutionContextFactory<IInstrumentedCDSPluginExecutionContext>
    {  
        public IInstrumentedCDSPluginExecutionContext CreateCDSExecutionContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, IComponentTelemetryClient telemetryClient)
        {
            return new InstrumentedCDSPluginExecutionContext(serviceProvider, container, executionContext as IPluginExecutionContext, telemetryClient);
        }

       
    }
}
