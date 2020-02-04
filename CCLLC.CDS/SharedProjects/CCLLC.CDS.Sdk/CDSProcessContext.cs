using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;

    public abstract class CDSProcessContext : ICDSProcessContext 
    {
        protected internal CDSProcessContext(IExecutionContext executionContext, IIocContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            this.Container = container;

            if (executionContext == null) throw new ArgumentNullException("executionContext");
            this.ExecutionContext = executionContext;
        }

        public IReadOnlyIocContainer Container { get; private set; }
        protected IExecutionContext ExecutionContext { get; private set; }

        private IOrganizationServiceFactory organizationServiceFactory;
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
        public IEnhancedOrganizationService OrganizationService
        {
            get
            {
                if (organizationService == null)
                {                   
                    organizationService = new EnhancedOrganizationService(this.OrganizationServiceFactory.CreateOrganizationService(this.ExecutionContext.UserId));
                }

                return organizationService;
            }
        }

        private IEnhancedOrganizationService elevatedOrganizationService = null;
        /// <summary>
        /// Access to an organization service that runs with elevated access credentials under 
        /// the SYSTEM user identity.
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
        public ISettingsProvider Settings
        {
            get
            {
                if (settings is null)
                {
                    var factory = Container.Resolve<IExtensionSettingsFactory>();
                    settings = factory.CreateExtensionSettings(this.ElevatedOrganizationService);
                }
                return settings;
            }
        }
        
      
        private IXmlConfigurationResource xmlConfigurationResources = null;
        /// <summary>
        /// Access to configuration resources stored in CRM Xml Data Web Resources.
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


        public Entity TargetEntity
        {
            get
            {
                if (this.ExecutionContext.InputParameters.Contains("Target"))
                {
                    return this.ExecutionContext.InputParameters["Target"] as Entity;                    
                }

                return null;
            }
        }

        /// <summary>
        /// Returns the 'Target' of the message as an EntityReference if available
        /// </summary>
        public EntityReference TargetReference
        {
            get
            {
                if (this.TargetEntity != null)
                {
                    return this.TargetEntity.ToEntityReference();
                }                   
                return null;
            }
        }


        private ITracingService tracingService;
        public ITracingService TracingService
        {
            get
            {
                if (tracingService == null) { tracingService = CreateTracingService(); }
                return tracingService;
            }
        }

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

        private ICDSWebRequestFactory _webRequestFactory;      
        public virtual IHttpWebRequest CreateWebRequest(Uri address, string dependencyName = null)
        {
            if(_webRequestFactory is null)
            {
                _webRequestFactory = this.Container.Resolve<ICDSWebRequestFactory>();
            }
            return _webRequestFactory.CreateWebRequest(address, dependencyName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }     

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
        /// Writes a message to the pluign trace log.
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
