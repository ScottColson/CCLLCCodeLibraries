using System;

namespace CCLLC.Core.Net
{
    using CCLLC.Telemetry;

    /// <summary>
    /// Implements the capture of dependency telemetry as part of executing HTTP request actions.
    /// </summary>
    public class InstrumentedHttpWebRequestWrapper : HttpWebRequestWrapper
    {               
        private ITelemetryFactory _telemetryFactory = null;
        private ITelemetryClient _telemetryClient = null;
        private string _telemetryTag = null;

        public InstrumentedHttpWebRequestWrapper(ITelemetryFactory telemetryFactory, ITelemetryClient telementryClient, Uri address, string telemetryTag = null)
            : base(address)
        {            
            _telemetryTag = telemetryTag;
            _telemetryClient = telementryClient ?? throw new ArgumentNullException("telemetryClient is required.");
            _telemetryFactory = telemetryFactory ?? throw new ArgumentNullException("telemetryFactory is required.");
        }
            
       
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _telemetryTag = null;
            _telemetryClient = null;
            _telemetryFactory = null;           
        }

        public override IWebResponse Get()
        {
            IDependencyTelemetry dependencyTelemetry = null;
          
            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                 Address.ToString(),
                _telemetryTag ?? "WebRequest",
                null);

            using (var dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry))
            {
                var response = base.Get();

                dependencyTelemetry.ResultCode = response.StatusCode.ToString();
                dependencyClient.CompleteOperation(response.Success);

                return response;
            }
        }

        public override IWebResponse Delete()
        {
            IDependencyTelemetry dependencyTelemetry = null;

            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                Address.ToString(),
                _telemetryTag ?? "WebRequest",
                null);

            using (var dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry))
            {
                var response = base.Delete();

                dependencyTelemetry.ResultCode = response.StatusCode.ToString();
                dependencyClient.CompleteOperation(response.Success);

                return response;
            }
        }

        public override IWebResponse Post(byte[] data, string contentType = null, string contentEncoding = null)             
        {
            IDependencyTelemetry dependencyTelemetry = null;           
            
            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                Address.ToString(),
                _telemetryTag ?? "WebRequest",
                null);
             
            using(var dependencyClient = _telemetryClient.StartOperation<IDependencyTelemetry>(dependencyTelemetry))
            {
                var response = base.Post(data, contentType, contentEncoding);

                dependencyTelemetry.ResultCode = response.StatusCode.ToString();
                dependencyClient.CompleteOperation(response.Success);

                return response;
            }  
        }

        public override IWebResponse Put(string data, string contentType = null)
        {
            IDependencyTelemetry dependencyTelemetry = null;

            dependencyTelemetry = _telemetryFactory.BuildDependencyTelemetry(
                "Web",
                Address.ToString(),
                _telemetryTag ?? "WebRequest",
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
