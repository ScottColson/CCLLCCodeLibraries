using System;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    public class InstrumenetedCDSWebRequestFactory : IInstrumentedCDSWebRequestFactory
    {             
        public IHttpWebRequest CreateWebRequest(Uri address, string dependencyName, ITelemetryFactory telemetryFactory, ITelemetryClient telemetryClient)
        {
            return new InstrumentedHttpWebRequestWrapper(address, telemetryFactory,telemetryClient, dependencyName);
        }
    }
}
