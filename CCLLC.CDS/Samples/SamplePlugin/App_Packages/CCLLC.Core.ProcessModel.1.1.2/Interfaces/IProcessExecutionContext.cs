using System;

namespace CCLLC.Core
{
    public interface IProcessExecutionContext
    {
        IDataService DataService { get; }    

        ICache Cache { get; }

        IReadOnlyIocContainer Container { get; }

        ISettingsProvider Settings { get; }

        void Trace(string message, params object[] args);

        void Trace(eSeverityLevel severityLevel, string message, params object[] args);

        void TrackEvent(string name);

        void TrackException(Exception ex);
    }
}
