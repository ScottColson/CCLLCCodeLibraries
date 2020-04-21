using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;

    /// <summary>
    /// Enhanced execution context that implements requirements of <see cref="Microsoft.Xrm.Sdk.IExecutionContext"/> and
    /// <see cref="CCLLC.Core.IProcessExecutionContext"/> and adds additional functionality.
    /// </summary>
    public abstract class CDSExecutionContext : ICDSExecutionContext 
    {
        protected internal CDSExecutionContext(IExecutionContext executionContext, IIocContainer container, eRunAs runAs = eRunAs.User)
        {
            this.Container = container ?? throw new ArgumentNullException("container");            
            this.ExecutionContext = executionContext ?? throw new ArgumentNullException("executionContext");
        }

        /// <summary>
        /// Flag indicating whether the <see cref="OrganizationService"/> property returns an <see cref="IOrganizationService"/>
        /// object that runs as the initiating user or the system.
        /// </summary>
        public eRunAs RunAs { get; }

        /// <summary>
        /// Provides read only access to the <see cref="IIocContainer"/> in the plugin.
        /// </summary>
        public IReadOnlyIocContainer Container { get; private set; }

        /// <summary>
        /// Protected getter for the wrapped <see cref="IExecutionContext"/> object.
        /// </summary>
        protected IExecutionContext ExecutionContext { get; private set; }

        private IOrganizationServiceFactory organizationServiceFactory;

        /// <summary>
        /// Protected getter for the <see cref="IOrganizationServiceFactory"/> object.
        /// </summary>
        protected IOrganizationServiceFactory OrganizationServiceFactory
        {
            get
            {                
                if (organizationServiceFactory == null)
                {
                    organizationServiceFactory = CreateOrganizationServiceFactory();
                }
                return organizationServiceFactory;
            }
        }
        
        private IEnhancedOrganizationService organizationService;

        /// <summary>
        /// Instance of <see cref="IEnhancedOrganizationService"/> that runs as the user identified by <see cref="UserId"/> except when
        /// <see cref="RunAs"/> is set to <see cref="eRunAs.System"/> the <see cref="IEnhancedOrganizationService"/> object
        /// will run as the System user.
        /// </summary>
        public IEnhancedOrganizationService OrganizationService
        {
            get
            {
                if (RunAs == eRunAs.System) return ElevatedOrganizationService;

                if (organizationService == null)
                {                   
                    organizationService = new EnhancedOrganizationService(this.OrganizationServiceFactory.CreateOrganizationService(this.UserId));
                }

                return organizationService;
            }
        }

        private IEnhancedOrganizationService elevatedOrganizationService = null;
 
        /// <summary>
        /// Instance of <see cref="IEnhancedOrganizationService"/> that runs as the
        /// System user regardless of the setting of <see cref="RunAs"/>.
        /// </summary>
        public IEnhancedOrganizationService ElevatedOrganizationService
        {
            get
            {
                if (elevatedOrganizationService == null)
                {                   
                    elevatedOrganizationService = new EnhancedOrganizationService(this.OrganizationServiceFactory.CreateOrganizationService(null));
                }

                return elevatedOrganizationService;
            }
        }

        /// <summary>
        /// Access to the <see cref="ICache"/> implementation.
        /// </summary>
        ICache cache;
        public ICache Cache
        {
            get
            {
                if (cache is null)
                {
                    cache = Container.Resolve<ICache>();
                }
                return cache;
            }
        }
             

        private ISettingsProvider settings = null;

        /// <summary>
        /// Access to the <see cref="ISettingsProvider"/> for using name/value pair
        /// settings.
        /// </summary>
        public ISettingsProvider Settings
        {
            get
            {
                if (settings is null)
                {
                    var factory = Container.Resolve<ISettingsProviderFactory>();
                    settings = factory.CreateSettingsProvider(this);
                }
                return settings;
            }
        }
        
      
        private IXmlConfigurationResource xmlConfigurationResources = null;
        /// <summary>
        /// Access to configuration resources stored in CRM XML Data Web Resources.
        /// </summary>
        public IXmlConfigurationResource XmlConfigurationResources
        {
            get
            {
                if (xmlConfigurationResources == null)
                {
                    var factory = Container.Resolve<IXmlConfigurationResourceFactory>();
                    xmlConfigurationResources = factory.CreateConfigurationResources(this.ElevatedOrganizationService);
                }

                return xmlConfigurationResources;
            }
        }

        /// <summary>
        /// Returns the 'Target' entity passed in the <see cref="InputParameters"/> "Target" parameter. 
        /// </summary>
        public Entity TargetEntity
        {
            get
            {
                if (this.InputParameters.Contains("Target") && this.InputParameters["Target"] is Entity)
                {
                    return this.InputParameters["Target"] as Entity;                    
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the 'Target' of the message as an EntityReference if available
        /// </summary>
        public EntityReference TargetReference => this.TargetEntity?.ToEntityReference();        


        private ITracingService tracingService;
        /// <summary>
        /// Access to the <see cref="ITracingService"/> for plugin or workflow execution.
        /// </summary>
        public ITracingService TracingService
        {
            get
            {
                if (tracingService == null) { tracingService = CreateTracingService(); }
                return tracingService;
            }
        }


        /// <summary>
        /// Access to the <see cref="IDataService"/> used by the <see cref="IProcessExecutionContext"/> 
        /// when working with code based on the CCLLC.Core.ProcessModel framework.
        /// </summary>
        public IDataService DataService => this.OrganizationService;

        public int Depth => this.ExecutionContext.Depth;

        public string MessageName => this.ExecutionContext.MessageName;

        public int Mode => this.ExecutionContext.Mode;

        public int IsolationMode => this.ExecutionContext.IsolationMode;

        public string PrimaryEntityName => this.ExecutionContext.PrimaryEntityName;

        public Guid? RequestId => this.ExecutionContext.RequestId;

        public string SecondaryEntityName => this.ExecutionContext.SecondaryEntityName;

        public ParameterCollection InputParameters => this.ExecutionContext.InputParameters;

        public ParameterCollection OutputParameters => this.ExecutionContext.OutputParameters;

        public ParameterCollection SharedVariables => this.ExecutionContext.SharedVariables;

        public Guid UserId => this.ExecutionContext.UserId;

        public Guid InitiatingUserId => this.ExecutionContext.InitiatingUserId;

        public Guid BusinessUnitId => this.ExecutionContext.BusinessUnitId;

        public Guid OrganizationId => this.ExecutionContext.OrganizationId;

        public string OrganizationName => this.ExecutionContext.OrganizationName;

        public Guid PrimaryEntityId => this.ExecutionContext.PrimaryEntityId;

        public EntityImageCollection PreEntityImages => this.ExecutionContext.PreEntityImages;

        public EntityImageCollection PostEntityImages => this.ExecutionContext.PostEntityImages;

        public EntityReference OwningExtension => this.ExecutionContext.OwningExtension;

        public Guid CorrelationId => this.ExecutionContext.CorrelationId;

        public bool IsExecutingOffline => this.ExecutionContext.IsExecutingOffline;

        public bool IsOfflinePlayback => this.ExecutionContext.IsOfflinePlayback;

        public bool IsInTransaction => this.ExecutionContext.IsInTransaction;

        public Guid OperationId => this.ExecutionContext.OperationId;

        public DateTime OperationCreatedOn => this.ExecutionContext.OperationCreatedOn;

        private IWebRequestFactory _webRequestFactory;    
        
        /// <summary>
        /// Generates a disposable <see cref="IWebRequest"/> object for simplified HTTP web request
        /// access inside a plugin.
        /// </summary>
        /// <param name="address">The URI of the web resource.</param>
        /// <param name="dependencyName">A dependence name to use for telemetry tracking.</param>
        /// <returns></returns>
        public virtual IWebRequest CreateWebRequest(Uri address, string dependencyName = null)
        {
            if(_webRequestFactory is null)
            {
                _webRequestFactory = this.Container.Resolve<IWebRequestFactory>();
            }
            return _webRequestFactory.CreateWebRequest(address, dependencyName);
        }

        /// <summary>
        /// Dispose of the execution context.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }     

        /// <summary>
        /// Protected disposal of context dependencies.
        /// </summary>
        /// <param name="dispossing"></param>
        protected virtual void Dispose(bool dispossing)
        {
            if (dispossing)
            {
                this.Container = null;
                this.elevatedOrganizationService = null;
                this.ExecutionContext = null;
                this.settings = null;                
                this.organizationService = null;
                this.organizationServiceFactory = null;
                this.cache = null;
                this.tracingService = null;
                this.xmlConfigurationResources = null;
                this._webRequestFactory = null;
            }          
        }

        protected abstract IOrganizationServiceFactory CreateOrganizationServiceFactory();
        protected abstract ITracingService CreateTracingService();

        /// <summary>
        /// Creates an early bound organization request of type T based on data stored in the <see cref="InputParameters"/>
        /// of the execution context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateOrganizationRequest<T>() where T : Microsoft.Xrm.Sdk.OrganizationRequest, new()
        {
            var req = new T();
            foreach (var p in InputParameters)
            {
                req.Parameters[p.Key] = p.Value;
            }

            return req;
        }

        /// <summary>
        /// Retrieves a single entity record as early bound type T,  containing all fields and optionally 
        /// caches the record when a cacheTimeout value is provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recordId"></param>
        /// <param name="cacheTimeout"></param>
        /// <returns></returns>
        public T GetRecord<T>(EntityReference recordId, TimeSpan? cacheTimeout = null) where T : Entity
        {
            return GetRecord<T>(recordId, null, cacheTimeout);
        }

        /// <summary>
        /// Retrieves a single entity record as early bound type T,  containing the specified fields and optionally 
        /// caches the record when a cacheTimeout value is provided.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="recordId"></param>
        /// <param name="cacheTimeout"></param>
        /// <returns></returns>
        public T GetRecord<T>(EntityReference recordId, string[] columns = null, TimeSpan? cacheTimeout = null) where T : Entity
        {
            string cacheKey = null;

            if(Cache != null &&  cacheTimeout != null)
            {
                cacheKey = string.Format("CCLLC.CDS.GetRecord.{0}{1}", recordId.LogicalName, recordId.Id);

                if (Cache.Exists(cacheKey))
                {
                    return Cache.Get<T>(cacheKey);
                }
            }

            var columnSet = columns == null ? new ColumnSet(true) : new ColumnSet(columns);

            var record = OrganizationService.Retrieve(
                recordId.LogicalName,
                recordId.Id,
                columnSet).ToEntity<T>();

            if(cacheKey != null)
            {
                Cache.Add<T>(cacheKey, record, cacheTimeout.Value);
            }

            return record;
        }

        /// <summary>
        /// Writes a message to the plugin trace log.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public virtual void Trace(string message, params object[] args)
        {
            this.Trace(eSeverityLevel.Information, message, args);
        }

        /// <summary>
        /// Writes a message to the plugin trace log.
        /// </summary>
        /// <param name="severityLevel"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public virtual void Trace(eSeverityLevel severityLevel, string message, params object[] args)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var msg = severityLevel.ToString() + ": " + message;
                this.TracingService.Trace(msg, args);               
            }        
        }

        /// <summary>
        /// Writes an exception entry to the plugin trace log.
        /// </summary>
        /// <param name="ex"></param>
        public virtual void TrackException(Exception ex)
        {
            this.Trace(eSeverityLevel.Error, "Exception: {0}", ex.Message);
        }

        /// <summary>
        /// Writes an event to the plugin trace log.
        /// </summary>
        /// <param name="name"></param>
        public virtual void TrackEvent(string name)
        {
            this.Trace(eSeverityLevel.Information, "Event: {0}", name);
        }

    }
}
