using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;

    public interface ICDSExecutionContext : IProcessExecutionContext, IExecutionContext, IDisposable 
    {        
        IEnhancedOrganizationService OrganizationService { get; }
        IEnhancedOrganizationService ElevatedOrganizationService { get; }       
        ITracingService TracingService { get;  }             
        IXmlConfigurationResource XmlConfigurationResources { get; }

        Entity TargetEntity { get; }

        EntityReference TargetReference { get; }

        IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null);
    }
}
