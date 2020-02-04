using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public enum ePluginStage
    {
        PreValidation = 10,
        PreOperation = 20,
        PostOperation = 40
    }

    public interface ICDSPluginExecutionContext : IPluginExecutionContext, ICDSExecutionContext
    {
        IServiceProvider ServiceProvider { get; }      
        new ePluginStage Stage { get; }        
        Entity PreImage { get; }
        Entity PostImage { get; }
        Entity PreMergedTarget { get; }
    }
}
