using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public static class FluentQueryExtensions
    {
        public static IList<E> Retrieve<E>(this IQueryEntity<IFluentQuery<IExecutableFluentQuery,E>,E> fluentQuery) where E : Entity
        {
            var executableQuery = fluentQuery as IExecutableFluentQuery<E>;

            return executableQuery.Retreive();
        }

        public static QueryExpression Build<E>(this IQueryEntity<IFluentQuery<IQueryExpressionBuilder, E>, E> fluentQuery) where E : Entity
        {
            var builder = fluentQuery as IQueryExpressionBuilder<E>;

            return builder.Build();
        }
    }
}
