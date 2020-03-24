using System;
using System.Linq;
using Microsoft.Xrm.Sdk;


namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core; 

    public class CDSPluginExecutionContext : CDSExecutionContext, ICDSPluginExecutionContext
    {
        public IServiceProvider ServiceProvider { get; private set; }
               
        public ePluginStage Stage => (ePluginStage)(base.ExecutionContext as IPluginExecutionContext). Stage;

        /// <summary>
        /// Returns the first registered 'Pre' image for the pipeline execution
        /// </summary>
        public Entity PreImage 
        {
            get
            {
                if (base.ExecutionContext.PreEntityImages.Any())
                {
                    return base.ExecutionContext.PreEntityImages[base.ExecutionContext.PreEntityImages.FirstOrDefault().Key];
                }
                return null;
            }
            
        } 

        /// <summary>
        /// Returns the first registered 'Post' image for the pipeline execution
        /// </summary>
        public Entity PostImage
        {
            get
            {
                if (base.ExecutionContext.PostEntityImages.Any())
                {
                    return base.ExecutionContext.PostEntityImages[base.ExecutionContext.PostEntityImages.FirstOrDefault().Key];
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
        public Entity PreMergedTarget
        {
            get
            {
                if (_preMergedTarget == null)
                {
                    _preMergedTarget = new Entity(PrimaryEntityName, PrimaryEntityId);
                    
                    _preMergedTarget.MergeWith(this.TargetEntity);
                    _preMergedTarget.MergeWith(this.PreImage);
                }

                return _preMergedTarget;
            }
        }

        int IPluginExecutionContext.Stage => (base.ExecutionContext as IPluginExecutionContext).Stage;

        public IPluginExecutionContext ParentContext => (base.ExecutionContext as IPluginExecutionContext).ParentContext;

        protected internal CDSPluginExecutionContext(IServiceProvider serviceProvider, IIocContainer container, IPluginExecutionContext executionContext, eRunAs runAs = eRunAs.User)
            : base(executionContext, container, runAs)
        {
            this.ServiceProvider = serviceProvider;
        }

        protected override void Dispose(bool dispossing)
        {
            if (dispossing)
            {
                this._preMergedTarget = null;
                this.ServiceProvider = null;
            }

            base.Dispose(dispossing);
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

