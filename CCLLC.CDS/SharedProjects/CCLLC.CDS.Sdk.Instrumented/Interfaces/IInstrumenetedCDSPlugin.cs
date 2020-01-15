
namespace CCLLC.CDS.Sdk
{
    using CCLLC.Telemetry;

    public interface IInstrumenetedCDSPlugin : ICDSPlugin
    {
        ITelemetrySink TelemetrySink { get; }
        bool ConfigureTelemetrySink(ICDSPluginProcessContext processContext);

        bool TrackExecutionPerformance { get; set; }

        bool FlushTelemetryAfterExecution { get; set; }
    }
}
