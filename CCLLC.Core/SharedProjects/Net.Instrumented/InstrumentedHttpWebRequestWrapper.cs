using System;

namespace CCLLC.Core.Net
{
    using CCLLC.Telemetry;

    public class InstrumentedHttpWebRequestWrapper : HttpWebRequestWrapper
    {               
        private ITelemetryFactory _telemetryFactory = null;
        private ITelemetryClient _telemetryClient = null;
        private string _dependencyName = null;

        public InstrumentedHttpWebRequestWrapper(Uri address, ITelemetryFactory telemetryFactory, ITelemetryClient telementryClient, string dependencyName = null)
            : base(address)
        {            
            _dependencyName = dependencyName;
            _telemetryClient = telementryClient ?? throw new ArgumentNullException("telemetryClient is required.");
            _telemetryFactory = telemetryFactory ?? throw new ArgumentNullException("telemetryFactory is required.");
        }
            
       
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _dependencyName = null;
            _telemetryClient = null;
            _telemetryFactory = null;           
        }

        public override IHttpWebResponse Get()
        {
            IDependencyTelemetry dependencyTelemetry = null;
          
            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                 Address.ToString(),
                _dependencyName != null ? _dependencyName : "WebRequest",
                null);

            using (var dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry))
            {
                var response = base.Get();

                dependencyTelemetry.ResultCode = response.StatusCode.ToString();
                dependencyClient.CompleteOperation(response.Success);

                return response;
            }
        }
      

        public override IHttpWebResponse Post(byte[] data, string contentType = null, string contentEncoding = null)             
        {
            IDependencyTelemetry dependencyTelemetry = null;           
            
            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                Address.ToString(),
                _dependencyName != null ? _dependencyName : "WebRequest",
                null);
             
            using(var dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry))
            {
                var response = base.Post(data, contentType, contentEncoding);

                dependencyTelemetry.ResultCode = response.StatusCode.ToString();
                dependencyClient.CompleteOperation(response.Success);

                return response;
            }  
        }

        public override IHttpWebResponse Put(string data, string contentType = null)
        {
            IDependencyTelemetry dependencyTelemetry = null;

            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                Address.ToString(),
                _dependencyName != null ? _dependencyName : "WebRequest",
                null);

            using (var dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry))
            {
                var response = base.Put(data, contentType);

                dependencyTelemetry.ResultCode = response.StatusCode.ToString();
                dependencyClient.CompleteOperation(response.Success);

                return response;
            }
        }

    }
}
