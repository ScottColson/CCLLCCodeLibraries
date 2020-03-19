using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Test
{
    /// <summary>
    /// Applies pre-processing steps prior to an IOrganizationService method call.
    /// </summary>
    public class OrganizationServicePreProcessor : IOrganizationService
    {       
        private IOrganizationService Service { get; }       
        private List<Func<IOrganizationService, Entity, Entity>> CreatePreProcessors { get; }        
        private List<Func<IOrganizationService, Entity, Entity>> UpdatePreProcessors { get; }

        protected internal OrganizationServicePreProcessor(IOrganizationService service)
        {
            Service = service;
            CreatePreProcessors = new List<Func<IOrganizationService, Entity, Entity>>();
            UpdatePreProcessors = new List<Func<IOrganizationService, Entity, Entity>>();
        }     
        
        protected internal void AddCreatePreProcessors(params Func<IOrganizationService, Entity, Entity>[] preProcessors)
        {
            CreatePreProcessors.AddRange(preProcessors);
        }

        protected internal void AddUpdatePreProcessors(params Func<IOrganizationService, Entity, Entity>[] preProcessors)
        {
            UpdatePreProcessors.AddRange(preProcessors);
        }

        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            Service.Associate(entityName,entityId, relationship, relatedEntities);
        }

        public Guid Create(Entity entity)
        {
            foreach(var p in CreatePreProcessors)
            {
                entity = p.Invoke(Service, entity);
            }

            return Service.Create(entity);
        }

        public void Delete(string entityName, Guid id)
        {
            Service.Delete(entityName, id);
        }

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            Service.Disassociate(entityName, entityId, relationship, relatedEntities);
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return Service.Execute(request);
        }

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return Service.Retrieve(entityName, id, columnSet);
        }

        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return Service.RetrieveMultiple(query);
        }

        public void Update(Entity entity)
        {
            foreach(var p in UpdatePreProcessors)
            {
                entity = p.Invoke(Service, entity);
            }

            Service.Update(entity);
        }
       
    }
}
