using System;

namespace CCLLC.Core.Net
{
    using CCLLC.Telemetry;

    /// <summary>
    /// Factory for creating Instrumented Http Web Request objects.
    /// </summary>
    public class InstrumenetedWebRequestFactory : IInstrumentedWebRequestFactory
    {
        private readonly ITelemetryFactory TelemetryFactory;

        public InstrumenetedWebRequestFactory(ITelemetryFactory telemetryFactory)
        {
            TelemetryFactory = telemetryFactory;
        }

        public IWebRequest CreateWebRequest(ITelemetryClient telemetryClient, Uri address, string telemetryTag = null)
        {
            return new InstrumentedHttpWebRequestWrapper(TelemetryFactory, telemetryClient, address, telemetryTag);
        }

        public IWebRequest CreateWebRequest(ITelemetryClient telemetryClient, IAPIEndpoint endpoint, string telemetryTag = null)
        {
            return new InstrumentedHttpWebRequestWrapper(TelemetryFactory, telemetryClient, endpoint.ToUri(), telemetryTag);
        }
    }
}
