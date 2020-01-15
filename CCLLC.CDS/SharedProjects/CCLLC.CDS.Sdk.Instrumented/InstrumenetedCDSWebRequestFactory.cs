using System;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    public class InstrumenetedCDSWebRequestFactory : IInstrumentedCDSWebRequestFactory
    {      
        public IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null)
        {
            return new HttpWebRequestWrapper(address);
        }

        public IHttpWebRequest CreateWebRequest(Uri address, string dependencyName, ITelemetryFactory telemetryFactory, ITelemetryClient telemetryClient)
        {
            throw new NotImplementedException();
        }
    }
}
