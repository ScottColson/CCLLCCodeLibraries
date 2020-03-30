using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    //public interface IQueryExpressionBuilder : IFluentQuery
    //{       
       
    //}

    public interface IQueryExpressionBuilder<E> : IFluentQuery<IQueryExpressionBuilder<E>,E> where E : Entity 
    {
        QueryExpression Build();
    }
}
