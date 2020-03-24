using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Telemetry;
    using CCLLC.Telemetry.EventLogger;
    using CCLLC.Telemetry.Client;
    using CCLLC.Telemetry.Context;
    using CCLLC.Telemetry.Serializer;
    using CCLLC.Telemetry.Sink;

    /// <summary>
    /// Base plugin class for plugins using <see cref="ICDSPlugin"/> functionality with telemetry logging outside 
    /// of Dynamics 365.
    /// </summary>
    public abstract class InstrumentedCDSPlugin : CDSPlugin, IInstrumenetedCDSPlugin
    {
               
        /// <summary>
        /// Provides a <see cref="ITelemetrySink"/> to receive and process various 
        /// <see cref="ITelemetry"/> items generated during the execution of the 
        /// Plugin.
        /// </summary>
        public virtual ITelemetrySink TelemetrySink { get; protected set; }
        
        /// <summary>
        /// Constructor for <see cref="InstrumentedCDSPlugin"/>.
        /// </summary>
        /// <param name="unsecureConfig"></param>
        /// <param name="secureConfig"></param>
        public InstrumentedCDSPlugin(string unsecureConfig, string secureConfig) 
            : base(unsecureConfig, secureConfig)
        {           
            // Dependencies for instrumented execution context.
            Container.Implement<IInstrumentedCDSExecutionContextFactory<IInstrumentedCDSPluginExecutionContext>>().Using<InstrumentedCDSExecutionContextFactory>().AsSingleInstance();
            Container.Implement<IInstrumentedCDSWebRequestFactory>().Using<InstrumenetedCDSWebRequestFactory>();

            // Telemetry issue event logger. Use inert logger for plugins because we don't have
            // the required security level to interact directly with the event log.
            Container.Implement<IEventLogger>().Using<InertEventLogger>().AsSingleInstance();

            // Setup the objects needed to create/capture telemetry items.
            Container.Implement<ITelemetryFactory>().Using<TelemetryFactory>().AsSingleInstance();  //ITelemetryFactory is used to create new telemetry items.
            Container.Implement<ITelemetryClientFactory>().Using<TelemetryClientFactory>().AsSingleInstance(); //ITelemetryClientFactory is used to create and configure a telemetry client.
            Container.Implement<ICDSTelemetryPropertyManager>().Using<ExecutionContextPropertyManager>().AsSingleInstance(); //Plugin property manager.
            Container.Implement<ITelemetryContext>().Using<TelemetryContext>(); //ITelemetryContext is a dependency for telemetry creation.
            Container.Implement<ITelemetryInitializerChain>().Using<TelemetryInitializerChain>(); //ITelemetryInitializerChain is a dependency for building a telemetry client.

            // Setup the objects needed to buffer and send telemetry to Application Insights. TelemetrySink
            // is setup as a single instance so that all plugins share the same sink. This may not always be
            // desirable. If multiple TelemetrySinks are needed then overwrite this implementation in the 
            // inheriting class.
            Container.Implement<ITelemetrySink>().Using<TelemetrySink>().AsSingleInstance(); //ITelemetrySink receives telemetry from one or more telemetry clients, processes it, buffers it, and transmits it.
            Container.Implement<ITelemetryProcessChain>().Using<TelemetryProcessChain>(); //ITelemetryProcessChain holds 0 or more processors that can modify the telemetry prior to transmission.
            Container.Implement<ITelemetryChannel>().Using<SyncMemoryChannel>(); //ITelemetryChannel provides the buffering and transmission. There is a sync and an asynch channel.
            Container.Implement<ITelemetryBuffer>().Using<TelemetryBuffer>(); //ITelemetryBuffer is used the channel
            Container.Implement<ITelemetryTransmitter>().Using<AITelemetryTransmitter>(); //ITelemetryTransmitter transmits a block of telemetry to Application Insights.

            // Setup the objects needed to serialize telemetry as part of transmission.
            Container.Implement<IContextTagKeys>().Using<AIContextTagKeys>(); //Defines context tags expected by Application Insights.
            Container.Implement<ITelemetrySerializer>().Using<AITelemetrySerializer>(); //Serialize telemetry items into a compressed Gzip data.
            Container.Implement<IJsonWriterFactory>().Using<JsonWriterFactory>(); //Factory to create JSON converters as needed.

        }
              

        /// <summary>
        /// Flag to capture execution performance metrics using request telemetry.
        /// </summary>
        public bool TrackExecutionPerformance { get; set; }

        /// <summary>
        /// Flag to force flushing the telemetry sink buffer after handler execution completes. When
        /// set to false the <see cref="TelemetrySink"/> transmits every 30 seconds while the plugin
        /// is in memory, every 1000 pieces of telemetry, and just prior to plugin disposal. 
        /// </summary>
        public bool FlushTelemetryAfterExecution { get; set; }

        /// <summary>
        /// Sets a the default key used when sending telemetry data from the plugin. Can be overridden using
        /// setting "Telemetry.InstrumentationKey". If no instrumentation key is provided then telemetry
        /// capture is disabled. 
        /// </summary>
        public string DefaultInstrumentationKey { get; set; }

        /// <summary>
        /// Telemetry Sink that gathers and transmits telemetry.
        /// </summary>
        /// <param name="cdsExecutionContext"></param>
        /// <returns></returns>
        public virtual bool ConfigureTelemetrySink(ICDSPluginExecutionContext cdsExecutionContext)
        {
            if (cdsExecutionContext != null)
            {
                var key = cdsExecutionContext.Settings.GetValue<string>("Telemetry.InstrumentationKey",this.DefaultInstrumentationKey);
               
                if (!string.IsNullOrEmpty(key))
                {
                    TelemetrySink.ProcessChain.TelemetryProcessors.Add(new SequencePropertyProcessor());
                    TelemetrySink.ProcessChain.TelemetryProcessors.Add(new InstrumentationKeyPropertyProcessor(key));
                         
                    return true; //telemetry sink is configured.
                }
            }

            return false; //telemetry sink is not configured.
        }

        /// <summary>
        /// Executes the plug-in.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        /// Microsoft CRM plugins must be thread-safe and stateless. 
        /// </remarks>
        public override void Execute(IServiceProvider serviceProvider)
        {           
            var sw = System.Diagnostics.Stopwatch.StartNew();
            var success = true;
            var responseCode = "200";

            if (serviceProvider == null)
                throw new ArgumentNullException("serviceProvider");

            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Entering {0}.Execute()", this.GetType().ToString()));

            var executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // Setup sink if it has not already been done. This is done here to cover the possibility that an implementing class might override
            // the default, non-static, property implementation.
            var lockObj = new object();
            lock (lockObj)
            {
                if (TelemetrySink is null)
                {
                    TelemetrySink = Container.Resolve<ITelemetrySink>();
                }
            }
            

            var telemetryFactory = Container.Resolve<ITelemetryFactory>();
            var telemetryClientFactory = Container.Resolve<ITelemetryClientFactory>();

            //Create a dictionary of custom telemetry properties based on values in the execution context.
            var propertyManager = Container.Resolve<ICDSTelemetryPropertyManager>();
            var properties = propertyManager.CreatePropertiesDictionary(this.GetType().ToString(), executionContext);
            
            using (var telemetryClient = telemetryClientFactory.BuildClient(
                this.GetType().ToString(),
                this.TelemetrySink,
                properties))
            {

                #region Setup Telemetry Context

                //capture execution context attributes that fit in telemetry context
                telemetryClient.Context.Operation.Name = executionContext.MessageName;
                telemetryClient.Context.Operation.CorrelationVector = executionContext.CorrelationId.ToString();
                telemetryClient.Context.Operation.Id = executionContext.OperationId.ToString();
                telemetryClient.Context.Session.Id = executionContext.CorrelationId.ToString();

                //Capture data context if the telemetry provider supports data key context.
                var asDataContext = telemetryClient.Context as ISupportDataKeyContext;
                if (asDataContext != null)
                {
                    asDataContext.Data.RecordSource = executionContext.OrganizationName;
                    asDataContext.Data.RecordType = executionContext.PrimaryEntityName;
                    asDataContext.Data.RecordId = executionContext.PrimaryEntityId.ToString();
                }

                #endregion
                               
                try
                {
                    var matchingHandlers = this.PluginEventHandlers
                        .Where(a => (int)a.Stage == executionContext.Stage
                            && (string.IsNullOrWhiteSpace(a.MessageName) || string.Compare(a.MessageName, executionContext.MessageName, StringComparison.InvariantCultureIgnoreCase) == 0)
                            && (string.IsNullOrWhiteSpace(a.EntityName) || string.Compare(a.EntityName, executionContext.PrimaryEntityName, StringComparison.InvariantCultureIgnoreCase) == 0));

                    if (matchingHandlers.Any())
                    {
                        var factory = Container.Resolve<IInstrumentedCDSExecutionContextFactory<IInstrumentedCDSPluginExecutionContext>>();

                        using (var cdsExecutionContext = factory.CreateCDSExecutionContext(executionContext, serviceProvider, this.Container, telemetryClient))
                        {
                            if (!TelemetrySink.IsConfigured)
                            {
                                TelemetrySink.OnConfigure = () => { return this.ConfigureTelemetrySink(cdsExecutionContext); };
                            }

                            foreach (var handler in matchingHandlers)
                            {
                                try
                                {
                                    handler.PluginAction.Invoke(cdsExecutionContext);
                                }
                                catch (InvalidPluginExecutionException ex)
                                {
                                    success = false;
                                    responseCode = "400"; //indicates a business rule error
                                    if (telemetryClient != null && telemetryFactory != null)
                                    {
                                        telemetryClient.Track(telemetryFactory.BuildMessageTelemetry(ex.Message, eSeverityLevel.Error));
                                    }
                                    throw;
                                }
                                catch (Exception ex)
                                {
                                    success = false;
                                    responseCode = "500"; //indicates a server error
                                    if (telemetryClient != null && telemetryFactory != null)
                                    {
                                        telemetryClient.Track(telemetryFactory.BuildExceptionTelemetry(ex));
                                    }
                                    throw;
                                }
                                finally
                                {
                                    if (this.TrackExecutionPerformance && telemetryFactory != null && telemetryClient != null)
                                    {
                                        var r = telemetryFactory.BuildRequestTelemetry("PluginExecution", null, new Dictionary<string, string> { { "handlerName", handler.Id } });
                                        r.Duration = sw.Elapsed;
                                        r.ResponseCode = responseCode;
                                        r.Success = success;

                                        telemetryClient.Track(r);
                                    }

                                    if (this.FlushTelemetryAfterExecution && telemetryClient != null)
                                    {
                                        telemetryClient.Flush();
                                    }

                                    sw.Restart();
                                    
                                }
                            }
                        } //using localContext
                    }
                }
                catch (InvalidPluginExecutionException ex)
                {                    
                    tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                    throw;
                }
                catch (Exception ex)
                {                    
                    tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                    throw new InvalidPluginExecutionException(string.Format("Unhandled Plugin Exception {0}", ex.Message), ex);
                }
                
                
            } //using telemetryClient.

            sw.Stop();
            sw = null;

            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Exiting {0}.Execute()", this.GetType().ToString()));
        }
    }
}
