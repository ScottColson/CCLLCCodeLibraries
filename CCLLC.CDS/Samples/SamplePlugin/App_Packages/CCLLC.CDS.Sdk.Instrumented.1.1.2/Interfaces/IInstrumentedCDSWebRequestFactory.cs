using System;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    public interface IInstrumentedCDSWebRequestFactory
    {
        IHttpWebRequest CreateWebRequest(Uri address, string dependencyName, ITelemetryFactory telemetryFactory, ITelemetryClient telemetryClient);
    }
}
