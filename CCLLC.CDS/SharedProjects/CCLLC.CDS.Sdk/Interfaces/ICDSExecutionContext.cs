using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;

    /// <summary>
    /// Provides and enhanced execution context that is compatible with the <see cref="IExecutionContext"/> provided by the 
    /// Microsoft.Xrm.Sdk and the <see cref="IProcessExecutionContext"/> provided by CCLLC.Core.ProcessModel. The resulting
    /// execution context is disposable so it can be constructed and scoped as part of a Using clause.
    /// </summary>
    public interface ICDSExecutionContext : IProcessExecutionContext, IExecutionContext, IDisposable 
    {
        /// <summary>
        /// Execution flag that indicates whether the context runs under the security profile of the user or the system. When 
        /// set to System then the OrganizationService retrieved through the <see cref="OrganizationService"/> method runs 
        /// with elevated permissions.
        /// </summary>
        eRunAs RunAs { get; }

        /// <summary>
        /// Returns an <see cref="IEnhancedOrganizationService"/> implementation that wraps the standard
        /// <see cref="IOrganizationService"/> implementation provided by the Microsoft.Xrm.Sdk.
        /// </summary>
        IEnhancedOrganizationService OrganizationService { get; }

        /// <summary>
        /// Returns an <see cref="IEnhancedOrganizationService"/> implementation that wraps the standard
        /// <see cref="IOrganizationService"/> implementation provided by the Microsoft.Xrm.Sdk. The service
        /// runs with elevated permissions based on the System User.
        /// </summary>
        IEnhancedOrganizationService ElevatedOrganizationService { get; }

        /// <summary>
        /// Returns a standard <see cref="ITracingService"/> service.
        /// </summary>
        ITracingService TracingService { get;  }    
        
        /// <summary>
        /// Returns a <see cref="IXmlConfigurationResource"/> service that allows code to read in XML configuration data
        /// from an XML web resource.
        /// </summary>
        IXmlConfigurationResource XmlConfigurationResources { get; }

        /// <summary>
        /// Return the plugin target input parameter as an <see cref="Entity"/>.
        /// </summary>
        Entity TargetEntity { get; }

        /// <summary>
        /// Return the entity contained in the plugin target input parameter as an <see cref="EntityReference"/>.
        /// </summary>
        EntityReference TargetReference { get; }

        /// <summary>
        /// Creates a new organization request proxy of defined type loaded with parameters from the
        /// execution context input parameters collection. 
        /// </summary> 
        T CreateOrganizationRequest<T>() where T : OrganizationRequest, new();

        /// <summary>
        /// Creates a disposable HTTP web request object based on the <see cref="IHttpWebRequest"/> definition included in 
        /// the CCLLC.Core.Net name space.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="dependencyName"></param>
        /// <returns></returns>
        IWebRequest CreateWebRequest(Uri address, string dependencyName = null);

        /// <summary>
        /// Get one record returning all columns from the data store as a typed proxy and optionally cache it. Use overloaded
        /// call to set specific fields for the return set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recordId"></param>
        /// <param name="cacheTimeout"></param>
        /// <returns></returns>
        T GetRecord<T>(EntityReference recordId, TimeSpan? cacheTimeout = null) where T : Entity;

        /// <summary>
        /// Get one record returning the columns specified from the data store as a typed proxy and optionally cache it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recorddId"></param>
        /// <param name="columns"></param>
        /// <param name="cacheTimeout"></param>
        /// <returns></returns>
        T GetRecord<T>(EntityReference recorddId, string[] columns, TimeSpan? cacheTimeout = null) where T : Entity;
    }
}
