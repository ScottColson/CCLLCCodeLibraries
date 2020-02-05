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

        /// <summary>
        /// Creates a new organization request proxy of defined type loaded with parameters from the
        /// execution context input parameters collection. 
        /// </summary> 
        T CreateOrganizationRequest<T>() where T : OrganizationRequest, new();


        IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null);

        /// <summary>
        /// Get one record from the data store as a typed proxy and optionally cache it. Return all
        /// record columns or optionally define a set columns to include.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recordId"></param>
        /// <param name="columns"></param>
        /// <param name="cacheTimeout"></param>
        /// <returns></returns>
        T GetRecord<T>(EntityReference recordId, TimeSpan? cacheTimeout = null) where T : Entity;

        T GetRecord<T>(EntityReference recorddId, string[] columns, TimeSpan? cacheTimeout = null) where T : Entity;
    }
}
