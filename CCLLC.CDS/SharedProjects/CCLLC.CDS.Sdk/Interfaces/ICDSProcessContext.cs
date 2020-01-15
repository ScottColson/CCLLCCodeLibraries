using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;

    public interface ICDSProcessContext : IProcessContext, IDisposable 
    {                
        IExecutionContext ExecutionContext { get; }       
        IEnhancedOrganizationService OrganizationService { get; }
        IEnhancedOrganizationService ElevatedOrganizationService { get; }       
        ITracingService TracingService { get;  }     
        
        int Depth { get; }
        string MessageName { get; }       
       
        IXmlConfigurationResource XmlConfigurationResources { get; }

        Entity TargetEntity { get; }

        EntityReference TargetReference { get; }

        IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null);
    }
}
