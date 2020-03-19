using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;


namespace CCLLC.CDS.Test.Builders
{
    public class OrganizationServiceBuilder : DLaB.Xrm.Test.Builders.OrganizationServiceBuilderBase<OrganizationServiceBuilder>
    {
       
        private List<Func<IOrganizationService, Entity, Entity>> CreatePreProcessors { get; }
        private List<Func<IOrganizationService, Entity, Entity>> UpdatePreProcessors { get; }

        protected override OrganizationServiceBuilder This => this;

        #region Constructors

        public OrganizationServiceBuilder() 
            : this(GetOrganizationService()) { }

        public OrganizationServiceBuilder(IOrganizationService service) 
            : base(service)
        {            
            CreatePreProcessors = new List<Func<IOrganizationService, Entity, Entity>>();
            UpdatePreProcessors = new List<Func<IOrganizationService, Entity, Entity>>();

            CreatePreProcessors.Add(ConvertToSdkEntityPreProcessor);
            UpdatePreProcessors.Add(ConvertToSdkEntityPreProcessor);
        }

        #endregion Constructors

        #region Fluent Methods

        

        #endregion Fluent Methods

        public new IOrganizationService Build()
        {
            var service = base.Build();
            var preProcessor = new OrganizationServicePreProcessor(service);
            preProcessor.AddCreatePreProcessors(CreatePreProcessors.ToArray());
            preProcessor.AddUpdatePreProcessors(UpdatePreProcessors.ToArray());

            return preProcessor;
        }

        private Entity ConvertToSdkEntityPreProcessor(IOrganizationService service, Entity entity)
        {
            if (entity.GetType() == typeof(Entity))
            {
                return entity;
            }

            return entity.ToEntity<Entity>();
        }

        private static IOrganizationService GetOrganizationService()
        {
            return DLaB.Xrm.Test.TestBase.GetOrganizationService();
        }
    }
}