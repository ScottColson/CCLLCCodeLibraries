using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;
    using CCLLC.Core.Net;
 

    /// <summary>
    /// Base plugin class for plugins using <see cref="ICDSPlugin"/> functionality. This class does not provide
    /// telemetry logging outside of the CDS platform. For external telemetry use the CCLLC.CDS.Sdk.InstrumentedCDSPlugin
    /// from the CCLLC.CDS.Sdk.Instrumented package.
    /// </summary>
    public abstract class CDSPlugin : IPlugin, ICDSPlugin
    {
        private Collection<IPluginEventRegistration> _events = new Collection<IPluginEventRegistration>();
        private IIocContainer _container = null;

        /// <summary>
        /// Set whether processes for this plugin run as User or System. Default is User.
        /// </summary>
        public eRunAs RunAs { get; set; }

        /// <summary>
        /// Provides of list of <see cref="IPluginEventRegistration"/> items that define the 
        /// events the plugin can operate against. Items are added to the list using the 
        /// RegisterEvent method.
        /// </summary>
        protected IReadOnlyList<IPluginEventRegistration> PluginEventRegistrations
        {
            get
            {
                return this._events;
            }
        }


        /// <summary>
        /// Provides an <see cref="IIocContainer"/> instance to register all objects used by the
        /// base plugin. 
        /// </summary>
        public virtual IIocContainer Container
        {
            get
            {                
                if (_container == null) { _container = new IocContainer(); }
                return _container;
            }
        }


        /// <summary>
        /// Unsecure configuration specified during the registration of the plugin step
        /// </summary>
        public string UnsecureConfig { get; }

        /// <summary>
        /// Secure configuration specified during the registration of the plugin step
        /// </summary>
        public string SecureConfig { get; }


        /// <summary>
        /// Initializes a new instance of the <see cref="CDSPlugin"/> class 
        /// with configuration information.
        /// </summary>
        public CDSPlugin(string unsecureConfig, string secureConfig)
        {
            this.UnsecureConfig = unsecureConfig;
            this.SecureConfig = secureConfig;
            this.RunAs = eRunAs.User;

            // Register supporting implementations in the container.
            Container.Implement<ICache>().Using<DefaultCache>().AsSingleInstance();
            Container.Implement<ISettingsProviderFactory>().Using<SettingsProviderFactory>().AsSingleInstance();
            Container.Implement<ISettingsProviderDataConnector>().Using<EnvironmentVariablesDataConnector>().AsSingleInstance();
            Container.Implement<IXmlConfigurationResourceFactory>().Using<XmlConfigurationResourceFactory>().AsSingleInstance();
            Container.Implement<IWebRequestFactory>().Using<WebRequestFactory>().AsSingleInstance();
            Container.Implement<ICDSExecutionContextFactory<ICDSPluginExecutionContext>>().Using<CDSExecutionContextFactory>().AsSingleInstance();
        }

        /// <summary>
        /// Adds a new event handler registration to link a plugin event signature to a specific handler.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="messageName"></param>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="id"></param>
        public virtual void RegisterEventHandler(string entityName, string messageName, ePluginStage stage, Action<ICDSPluginExecutionContext> handler, string id="")
        {
            this._events.Add(new PluginEventRegistration
            {
                EntityName = entityName,
                MessageName = messageName,
                Stage = stage,
                PluginAction = handler,
                HandlerId = id
            });
        }

        /// <summary>
        /// Register a new event handler for create message using early bound entity types.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="handlerId"></param>
        public void RegisterCreateHandler<E>(ePluginStage stage, Action<ICDSPluginExecutionContext, E, EntityReference> handler, string handlerId = "") where E : Entity, new()
        {
            this._events.Add(new CreateEventRegistration<E>
            {
                HandlerId = handlerId,
                Stage = stage,
                PluginAction = handler
            });
        }

        /// <summary>
        /// Register a new event handler for an update message using early bound entity types.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="handlerId"></param>
        public void RegisterUpdateHandler<E>(ePluginStage stage, Action<ICDSPluginExecutionContext, E> handler, string handlerId = "") where E : Entity, new()
        {
            this._events.Add(new UpdateEventRegistration<E>
            {
                HandlerId = handlerId,
                Stage = stage,
                PluginAction = handler
            });
        }


        /// <summary>
        /// Register a new event handler for a delete message using early bound entity types.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="handlerId"></param>
        public void RegisterDeleteHandler<E>(ePluginStage stage, Action<ICDSPluginExecutionContext, EntityReference> handler, string handlerId = "") where E : Entity, new()
        {
            this._events.Add(new DeleteEventRegistration<E>
            {
                HandlerId = handlerId,
                Stage = stage,
                PluginAction = handler
            });
        }

        /// <summary>
        /// Register a new event handler for a retrieve message using early bound entity types.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="handlerId"></param>
        public void RegisterRetrieveHandler<E>(ePluginStage stage, Action<ICDSPluginExecutionContext, EntityReference, ColumnSet, E> handler, string handlerId = "") where E : Entity, new()
        {
            this._events.Add(new RetrieveEventRegistration<E>
            {
                HandlerId = handlerId,
                Stage = stage,
                PluginAction = handler
            });
        }

        /// <summary>
        /// Register a new event handler for a retrieve multiple message using early bound entity types.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="handlerId"></param>
        public void RegisterQueryHandler<E>(ePluginStage stage, Action<ICDSPluginExecutionContext, QueryExpression, EntityCollection> handler, string handlerId = "") where E : Entity, new()
        {
            this._events.Add(new QueryEventRegistration<E>
            {
                HandlerId = handlerId,
                Stage = stage,
                PluginAction = handler
            });
        }

        /// <summary>
        /// Register a new event handler for an action message using early bound action request and response types. Handler will
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="stage"></param>
        /// <param name="handler"></param>
        /// <param name="handlerId"></param>
        public void RegisterActionHandler<TRequest, TResponse>(ePluginStage stage, Action<ICDSPluginExecutionContext, TRequest, TResponse> handler, string handlerId = "")
            where TRequest : OrganizationRequest, new()
            where TResponse : OrganizationResponse, new()
        {
            this._events.Add(new ActionEventRegistration<TRequest, TResponse>
            {
                Stage = stage,
                HandlerId = handlerId,
                PluginAction = handler
            }); ;
        }



        /// <summary>
        /// Executes the plug-in.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <remarks>
        /// Microsoft CRM plugins must be thread-safe and stateless. 
        /// </remarks>
        public virtual void Execute(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException("serviceProvider");

            var tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Entering {0}.Execute()", this.GetType().ToString()));

            var executionContext = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            
            try
            {
                var matchingRegistrations = this.PluginEventRegistrations
                    .Where(a => (int)a.Stage == executionContext.Stage
                        && (string.IsNullOrWhiteSpace(a.MessageName) || string.Compare(a.MessageName, executionContext.MessageName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        && (string.IsNullOrWhiteSpace(a.EntityName) || string.Compare(a.EntityName, executionContext.PrimaryEntityName, StringComparison.InvariantCultureIgnoreCase) == 0));

                if (matchingRegistrations.Any())
                {
                    var factory = Container.Resolve<ICDSExecutionContextFactory<ICDSPluginExecutionContext>>();

                    using (var cdsExecutionContext = factory.CreateCDSExecutionContext(executionContext, serviceProvider, this.Container, this.RunAs))
                    {
                        foreach (var registration in matchingRegistrations)
                        {
                            registration.Invoke(cdsExecutionContext);
                        }
                    }
                }
            }
            catch(InvalidPluginExecutionException ex)
            {
                tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                throw;
            }
            catch (Exception ex)
            {
                tracingService.Trace(string.Format("Exception: {0}", ex.Message));
                throw new InvalidPluginExecutionException(string.Format("Unhandled Plugin Exception {0}", ex.Message), ex);
            }

            tracingService.Trace(string.Format(CultureInfo.InvariantCulture, "Exiting {0}.Execute()", this.GetType().ToString()));
        }
    }
}