using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    public abstract class InstrumentedCDSExecutionContext : CDSExecutionContext, IInstrumentedCDSExecutionContext
    {
        public IComponentTelemetryClient TelemetryClient { get; private set; }

        private ITelemetryFactory _telemetryFactory;
        public ITelemetryFactory TelemetryFactory
        {
            get
            {
                if (_telemetryFactory == null)
                {
                    _telemetryFactory = this.Container.Resolve<ITelemetryFactory>();
                }
                return _telemetryFactory;
            }
        }

        protected internal InstrumentedCDSExecutionContext(IExecutionContext executionContext, IIocContainer container, IComponentTelemetryClient telemetryClient) : base(executionContext, container)
        {
            this.TelemetryClient = telemetryClient ?? throw new ArgumentNullException("telemetryClient is required."); ;
        }

        private IInstrumentedCDSWebRequestFactory _webRequestFactory;
        public override IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null)
        {
            if (_webRequestFactory is null)
            {
                _webRequestFactory = this.Container.Resolve<IInstrumentedCDSWebRequestFactory>();
            }
            return _webRequestFactory.CreateWebRequest(address, dependencyName,  this.TelemetryFactory, this.TelemetryClient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.TelemetryClient != null)
                {
                    if (this.TelemetryClient.TelemetrySink != null)
                    {
                        this.TelemetryClient.TelemetrySink.OnConfigure = null;
                    }
                    this.TelemetryClient.Dispose();

                    this.TelemetryClient = null;
                    this._telemetryFactory = null;
                }
            }

            base.Dispose(disposing);

        }
               
        public virtual void SetAlternateDataKey(string name, string value)
        {
            if (this.TelemetryClient != null)
            {
                var asDataContext = this.TelemetryClient.Context as ISupportDataKeyContext;
                if (asDataContext != null)
                {
                    asDataContext.Data.AltKeyName = name;
                    asDataContext.Data.AltKeyValue = value;
                }
                else
                {
                    TelemetryClient.Context.Properties["alternate-key-name"] = name;
                    TelemetryClient.Context.Properties["alternate-key-value"] = value;
                }
            }
        }

        public override void Trace(CCLLC.Core.eSeverityLevel severityLevel, string message, params object[] args)
        {
            base.Trace(severityLevel, message, args);
            if (!string.IsNullOrEmpty(message))
            {
                if (this.TelemetryClient != null && this.TelemetryFactory != null)
                {
                    var level = (CCLLC.Telemetry.eSeverityLevel)(int)severityLevel;                  

                    var msgTelemetry = this.TelemetryFactory.BuildMessageTelemetry(string.Format(message, args), level);
                    this.TelemetryClient.Track(msgTelemetry);
                }
            }
        }

        public override void TrackEvent(string name)
        {            
            if(this.TelemetryFactory != null && this.TelemetryClient != null && !string.IsNullOrEmpty(name))
            {
                this.TelemetryClient.Track(this.TelemetryFactory.BuildEventTelemetry(name));
            }
        }

        public override void TrackException(Exception ex)
        {            
            if(this.TelemetryFactory != null && this.TelemetryClient !=null && ex != null)
            {
                this.TelemetryClient.Track(this.TelemetryFactory.BuildExceptionTelemetry(ex));
            }
        }
    }
}
