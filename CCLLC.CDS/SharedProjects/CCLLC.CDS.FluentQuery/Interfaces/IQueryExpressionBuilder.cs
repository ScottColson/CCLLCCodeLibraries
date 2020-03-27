using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public interface IQueryExpressionBuilder : IFluentQuery
    {       
        QueryExpression Build();
    }

    public interface IQueryExpressionBuilder<E> : IQueryExpressionBuilder, IFluentQuery<IQueryExpressionBuilder, E> where E : Entity 
    { 
    }
}
