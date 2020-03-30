using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public interface IQueryEntity { }

    public interface IQueryEntity<P, E> : IQueryEntity, IFilterable<P> where P : IQueryEntity<P,E>, IFilterable<P> where E : Entity
    {

        P LeftJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<P, E, RE>> expression) where RE : Entity, new();

        P InnerJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<P, E, RE>> expression) where RE : Entity, new();

        P Select(params string[] columns);

        P Select(Expression<Func<E, object>> anonymousTypeInitializer);

        P SelectAll();

        P OrderByAsc(params string[] columns);

        P OrderByDesc(params string[] columns);
    }
}
