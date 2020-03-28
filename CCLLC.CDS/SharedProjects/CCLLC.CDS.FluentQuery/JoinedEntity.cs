using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System.Collections.Generic;

namespace CCLLC.CDS.FluentQuery
{
    public class JoinedEntity<P, RE> : QueryEntity<P, RE>, IJoinedEntity<P, RE> where P : IQueryEntity where RE : Entity, new()
    {
        protected string Alias { get; private set; }
        protected JoinOperator JoinOperator { get; }
        protected string ParentEntity { get; }
        protected string ParentAttribute { get; }
        protected string RelatedEntity { get; }
        protected string RelatedAttribute { get; }

        public JoinedEntity(JoinOperator joinOperator, string parentEntityName, string parentAttributeName, string relatedEntityName, string relatedAttributeName, IQueryEntity parent) : base(parent) 
        {
            this.JoinOperator = joinOperator;
            this.ParentEntity = parentEntityName;
            this.ParentAttribute = parentAttributeName;
            this.RelatedEntity = relatedEntityName;
            this.RelatedAttribute = relatedAttributeName;
        }

        public IJoinedEntity<P, RE> WithAlias(string aliasName)
        {
            this.Alias = aliasName;
            return this;
        }

        public LinkEntity ToLinkEntity()
        {            
            var relatedRecordType = new RE().LogicalName;

            var linkEntity = new LinkEntity(ParentEntity, RelatedEntity, ParentAttribute, RelatedAttribute, JoinOperator);
            if (!string.IsNullOrEmpty(Alias))
            {
                linkEntity.EntityAlias = Alias;
            }
            
            return linkEntity;
            
        }
    }
}
