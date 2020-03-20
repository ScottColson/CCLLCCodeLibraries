using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    /// <summary>
    /// Defines an enhanced organization service that is compatible with the <see cref="IOrganizationService"/> provided
    /// by the Microsoft.Xrm.Sdk and the <see cref="IDataService"/> provided by CCLLC.Core.ProcessModel. This allows the enhanced
    /// service to pass through business logic layers that are data service agnostic.
    /// </summary>
    public class EnhancedOrganizationService : IEnhancedOrganizationService
    {
        private IOrganizationService orgService;   

        internal protected EnhancedOrganizationService(IOrganizationService orgService)
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
