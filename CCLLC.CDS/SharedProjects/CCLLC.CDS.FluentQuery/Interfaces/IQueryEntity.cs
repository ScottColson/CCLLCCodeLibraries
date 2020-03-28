using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public interface IQueryEntity : IFilterable
    {
        IList<LinkEntity> GetLinkedEntities();
    }

    public interface IQueryEntity<P,E> : IQueryEntity, IFilterable<IQueryEntity<P,E>> where P : IQueryEntity where E : Entity
    {

        IQueryEntity<P, E> LeftJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<IQueryEntity<P, E>, RE>> experession) where RE : Entity, new();

        IQueryEntity<P, E> InnerJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<IQueryEntity<P, E>, RE>> expression) where RE : Entity, new();

        IQueryEntity<P, E> Select(params string[] columns);

        IQueryEntity<P, E> Select(Expression<Func<E, object>> anonymousTypeInitializer);

        IQueryEntity<P, E> SelectAll();
    }
}
