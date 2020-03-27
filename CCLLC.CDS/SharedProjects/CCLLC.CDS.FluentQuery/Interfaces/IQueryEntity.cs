using System;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.FluentQuery
{
    public interface IQueryEntity : IFilterable
    {
    }

    public interface IQueryEntity<P,E> : IQueryEntity, IFilterable<IQueryEntity<P,E>> where P : IQueryEntity where E : Entity
    {

        IQueryEntity<P, E> LeftJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<P, E>> experession) where RE : Entity;

        IQueryEntity<P, E> InnerJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<P, E>> expression) where RE : Entity;

        IQueryEntity<P, E> Select(params string[] columns);

        IQueryEntity<P, E> Select(Expression<Func<E, object>> anonymousTypeInitializer);

        IQueryEntity<P, E> SelectAll();
    }
}
