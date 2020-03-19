using System;
using Microsoft.Xrm.Sdk;
using CCLLC.Core;

namespace CCLLC.CDS.Test.ExecutionContext
{
    public class Context : IProcessExecutionContext
    {
        public IDataService DataService { get; }

        public ICache Cache { get; }

        public IReadOnlyIocContainer Container { get; }

        public ISettingsProvider Settings { get; }

        public Context(IOrganizationService organizationService, IIocContainer iocContainer)
        {
            this.DataService = new EnhancedOrgService(organizationService);
            this.Container = iocContainer;
            this.Settings = new FakeExecutionSettings();
            this.Cache = new DefaultCache();
        }

        public void Trace(string message, params object[] args)
        {
            
        }

        public void Trace(eSeverityLevel severityLevel, string message, params object[] args)
        {
           
        }

        public void TrackEvent(string name)
        {
           
        }

        public void TrackException(Exception ex)
        {
           
        }
    }
}
