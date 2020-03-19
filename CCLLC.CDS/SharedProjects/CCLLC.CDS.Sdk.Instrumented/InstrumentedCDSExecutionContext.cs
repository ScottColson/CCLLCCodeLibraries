using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;
    using CCLLC.Telemetry;

    /// <summary>
    /// Extends <see cref="CDSExecutionContext"/> to provide telemetry functionality. 
    /// </summary>
    public abstract class InstrumentedCDSExecutionContext : CDSExecutionContext, IInstrumentedCDSExecutionContext
    {
        public IComponentTelemetryClient TelemetryClient { get; private set; }

        private ITelemetryFactory _telemetryFactory;

        /// <summary>
        /// Access to a factory for creating various types of telemetry for logging.
        /// </summary>
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

        protected internal InstrumentedCDSExecutionContext(IExecutionContext executionContext, IIocContainer container, IComponentTelemetryClient telemetryClient) 
            : base(executionContext, container)
        {
            this.TelemetryClient = telemetryClient ?? throw new ArgumentNullException("telemetryClient is required."); ;
        }

        private IInstrumentedCDSWebRequestFactory _webRequestFactory;

        /// <summary>
        /// Creates a web request that will log telemetry data to the plugin's Telemetry Sink.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dependencyName"></param>
        /// <returns></returns>
        public override IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null)
        {
            if (_webRequestFactory is null)
            {
                _webRequestFactory = this.Container.Resolve<IInstrumentedCDSWebRequestFactory>();
            }
            return _webRequestFactory.CreateWebRequest(address, dependencyName,  this.TelemetryFactory, this.TelemetryClient);
        }

        /// <summary>
        /// Dispose of the execution context and all dependencies.
        /// </summary>
        /// <param name="disposing"></param>
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
              
        /// <summary>
        /// Sets the alternate key/value pair that can be used for linking telemetry from other systems.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
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

        /// <summary>
        /// Capture trace message to the tracing log and also as message telemetry.
        /// </summary>
        /// <param name="severityLevel"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
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

        /// <summary>
        /// Capture the specified name as a telemetry event.
        /// </summary>
        /// <param name="name"></param>
        public override void TrackEvent(string name)
        {            
            if(this.TelemetryFactory != null && this.TelemetryClient != null && !string.IsNullOrEmpty(name))
            {
                this.TelemetryClient.Track(this.TelemetryFactory.BuildEventTelemetry(name));
            }
        }

        /// <summary>
        /// Capture exception information as telemetry.
        /// </summary>
        /// <param name="ex"></param>
        public override void TrackException(Exception ex)
        {            
            if(this.TelemetryFactory != null && this.TelemetryClient !=null && ex != null)
            {
                this.TelemetryClient.Track(this.TelemetryFactory.BuildExceptionTelemetry(ex));
            }
        }
    }
}
