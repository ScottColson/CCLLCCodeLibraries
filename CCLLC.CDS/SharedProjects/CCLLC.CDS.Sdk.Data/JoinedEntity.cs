using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CCLLC.CDS.Sdk
{
    public class JoinedEntity<P, E, RE> : QueryEntity<IJoinedEntity<P, E, RE>, RE>, IJoinedEntity<P, E, RE>, IJoinedEntitySettings<P, E, RE> where P : IQueryEntity<P, E> where E : Entity, new() where RE : Entity, new()
    {
        protected string Alias { get; private set; }
        protected JoinOperator JoinOperator { get; }
        protected string ParentEntity { get; }
        protected string ParentAttribute { get; }
        protected string RelatedEntity { get; }
        protected string RelatedAttribute { get; }

        public IJoinedEntitySettings<P, E, RE> With => this;

        public JoinedEntity(JoinOperator joinOperator, string parentEntityName, string parentAttributeName, string relatedEntityName, string relatedAttributeName, IQueryEntity<P,E> parent) : base() 
        {
            this.JoinOperator = joinOperator;
            this.ParentEntity = parentEntityName;
            this.ParentAttribute = parentAttributeName;
            this.RelatedEntity = relatedEntityName;
            this.RelatedAttribute = relatedAttributeName;
        }

        public LinkEntity ToLinkEntity()
        {            
            var relatedRecordType = new RE().LogicalName;

            var linkEntity = new LinkEntity(ParentEntity, RelatedEntity, ParentAttribute, RelatedAttribute, JoinOperator);

            linkEntity.EntityAlias = string.IsNullOrEmpty(Alias) ? RelatedEntity : Alias;

            linkEntity.Columns = GetColumnSet();
            linkEntity.LinkCriteria = GetFilterExpression();
            linkEntity.LinkEntities.AddRange(LinkEntities);
            linkEntity.Orders.AddRange(OrderExpressions);

            return linkEntity;
            
        }

        IJoinedEntity<P, E, RE> IJoinedEntitySettings<P, E, RE>.Alias(string aliasName)
        {
            this.Alias = aliasName;
            return this;
        }
    }
}
