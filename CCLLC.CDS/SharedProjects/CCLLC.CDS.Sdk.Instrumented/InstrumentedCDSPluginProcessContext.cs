using System;
using System.Linq;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using Extensions;
    using CCLLC.Core;
    using CCLLC.Telemetry;

    public class InstrumentedCDSPluginProcessContext : InstrumentedCDSProcessContext, IInstrumentedCDSPluginProcessContext
    {
        public IServiceProvider ServiceProvider { get; private set; }

        new public virtual IPluginExecutionContext ExecutionContext { get { return (IPluginExecutionContext)base.ExecutionContext; } }

        public virtual ePluginStage Stage { get { return (ePluginStage)this.ExecutionContext.Stage; } }

        /// <summary>
        /// Returns the first registered 'Pre' image for the pipeline execution
        /// </summary>
        public virtual Entity PreImage
        {
            get
            {
                if (this.ExecutionContext.PreEntityImages.Any())
                {
                    return this.ExecutionContext.PreEntityImages[this.ExecutionContext.PreEntityImages.FirstOrDefault().Key];
                }
                return null;
            }
        }

        /// <summary>
        /// Returns the first registered 'Post' image for the pipeline execution
        /// </summary>
        public virtual Entity PostImage
        {
            get
            {
                if (this.ExecutionContext.PostEntityImages.Any())
                {
                    return this.ExecutionContext.PostEntityImages[this.ExecutionContext.PostEntityImages.FirstOrDefault().Key];
                }
                return null;
            }
        }

        private Entity _preMergedTarget = null;
        /// <summary>
        /// Returns an Entity record with all attributes from the current inbound target and any additional attributes
        /// that might exist in the Pre Image entity if provided. PreMergedTarget is cached so future calls
        /// return the same entity object and will not reflect changes made to that Target since initial
        /// request.
        /// </summary>
        public virtual Entity PreMergedTarget
        {
            get
            {
                if (_preMergedTarget == null)
                {
                    _preMergedTarget = new Entity(this.ExecutionContext.PrimaryEntityName);
                    _preMergedTarget.Id = this.ExecutionContext.PrimaryEntityId;
                    _preMergedTarget.MergeWith(this.TargetEntity);
                    _preMergedTarget.MergeWith(this.PreImage);
                }

                return _preMergedTarget;
            }
        }

        protected internal InstrumentedCDSPluginProcessContext(IServiceProvider serviceProvider, IIocContainer container, IPluginExecutionContext executionContext, IComponentTelemetryClient telemetryClient)
            : base(executionContext, container, telemetryClient)
        {
            this.ServiceProvider = serviceProvider;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._preMergedTarget = null;
                this.ServiceProvider = null;
            }
            
            base.Dispose(disposing);
        }

        protected override IOrganizationServiceFactory CreateOrganizationServiceFactory()
        {
            return (IOrganizationServiceFactory)this.ServiceProvider.GetService(typeof(IOrganizationServiceFactory));
        }

        protected override ITracingService CreateTracingService()
        {
            return (ITracingService)this.ServiceProvider.GetService(typeof(ITracingService));
        }
    }

}

