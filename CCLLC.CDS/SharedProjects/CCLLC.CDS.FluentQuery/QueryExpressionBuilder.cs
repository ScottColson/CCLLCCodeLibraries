using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public class QueryExpressionBuilder<E> : FluentQuery<IQueryExpressionBuilder,E>, IQueryExpressionBuilder<E> where E : Entity, new()
    {
        public QueryExpressionBuilder() : base() { }

        public QueryExpression Build()
        {
            return this.getQueryExpression();
        }
    }
}
