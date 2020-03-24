namespace CCLLC.CDS.Sdk
{
    using CCLLC.Telemetry;
    

    public interface IInstrumentedCDSExecutionContext : ICDSExecutionContext
    {
        IComponentTelemetryClient TelemetryClient { get; }
        ITelemetryFactory TelemetryFactory { get; }
        void SetAlternateDataKey(string name, string value);        
    }
}
