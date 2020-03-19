using System;
using Microsoft.Xrm.Sdk;
using CCLLC.Core;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Test.ExecutionContext
{
    public class EnhancedOrgService : IDataService, IOrganizationService
    {
        private IOrganizationService orgService;

        internal protected EnhancedOrgService(IOrganizationService orgService)
        {
            this.orgService = orgService;
        }

        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            orgService.Associate(entityName, entityId, relationship, relatedEntities);
        }

        public Guid Create(Entity entity)
        {
            return orgService.Create(entity);
        }

        public void Delete(string entityName, Guid id)
        {
            orgService.Delete(entityName, id);
        }

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            orgService.Disassociate(entityName, entityId, relationship, relatedEntities);
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return orgService.Execute(request);
        }

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return orgService.Retrieve(entityName, id, columnSet);
        }

        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return orgService.RetrieveMultiple(query);
        }

        public void Update(Entity entity)
        {
            orgService.Update(entity);
        }
    }
}
