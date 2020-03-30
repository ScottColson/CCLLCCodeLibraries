using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.Sdk
{
    public abstract class FluentQuery<P,E> : QueryEntity<P,E>, IFluentQuery<P,E> where P : IFluentQuery<P,E> where E : Entity, new()
    {       

        public FluentQuery() : base()
        {          
        }

        
        //public override P WhereAll(Action<IFilter<P>> expression)
        //{
        //    // The FluentQuery is the top of the tree and has no parent so return this rather than 
        //    // the default parent of the Fluent abstract class.
        //    base.WhereAll(expression);
        //    return (P)Parent;
        //}

        //public override P WhereAny(Action<IFilter<P>> expression)
        //{
        //    // The FluentQuery is the top of the tree and has no parent so return this rather than 
        //    // the default parent of the Fluent abstract class.
        //    base.WhereAny(expression);
        //    return this;
        //}

        protected QueryExpression getQueryExpression()
        {
            E baseRecord = new E();

            var qryExpression = new QueryExpression(baseRecord.LogicalName);

            qryExpression.ColumnSet = GetColumnSet();
            qryExpression.Criteria = GetFilterExpression();
            qryExpression.LinkEntities.AddRange(LinkEntities);
            qryExpression.Orders.AddRange(OrderExpressions);

            return qryExpression;
        }

    }
}
