using System;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    /// <summary>
    /// Factory for creating Instrumented Http Web Request objects.
    /// </summary>
    public class InstrumenetedCDSWebRequestFactory : IInstrumentedCDSWebRequestFactory
    {             
        public IHttpWebRequest CreateWebRequest(Uri address, string dependencyName, ITelemetryFactory telemetryFactory, ITelemetryClient telemetryClient)
        {
            return new InstrumentedHttpWebRequestWrapper(address, telemetryFactory,telemetryClient, dependencyName);
        }
    }
}
