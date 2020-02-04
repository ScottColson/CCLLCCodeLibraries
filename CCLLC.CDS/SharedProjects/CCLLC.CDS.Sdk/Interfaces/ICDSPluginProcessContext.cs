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

    public interface ICDSPluginProcessContext : ICDSProcessContext
    {
        IServiceProvider ServiceProvider { get; }      
        ePluginStage Stage { get; }
        new IPluginExecutionContext ExecutionContext { get; }        
        Entity PreImage { get; }
        Entity PostImage { get; }
        Entity PreMergedTarget { get; }
    }
}
