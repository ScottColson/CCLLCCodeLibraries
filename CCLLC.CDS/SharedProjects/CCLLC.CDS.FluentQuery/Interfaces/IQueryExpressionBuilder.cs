using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    //public interface IQueryExpressionBuilder : IFluentQuery
    //{       
       
    //}

    public interface IQueryExpressionBuilder<E> : IFluentQuery<IQueryExpressionBuilder<E>,E> where E : Entity 
    {
        QueryExpression Build();
    }
}
