using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.FluentQuery
{
    public abstract class FluentQuery<T,E> : QueryEntity<IFluentQuery<T,E>,E>, IFluentQuery<T,E> where T : IFluentQuery where E : Entity, new()
    {       

        public FluentQuery() : base(null)
        {          
        }

        public override IQueryEntity<IFluentQuery<T, E>, E> WhereAll(Action<IFilter<IFilterable<IQueryEntity<IFluentQuery<T, E>, E>>>> experssion)
        {
            // The FluentQuery is the top of the tree and has no parent so return this rather than 
            // the default parent of the Fluent abstract class.
            base.WhereAll(experssion);
            return this;
        }

        public override IQueryEntity<IFluentQuery<T, E>, E> WhereAny(Action<IFilter<IFilterable<IQueryEntity<IFluentQuery<T, E>, E>>>> experssion)
        {
            // The FluentQuery is the top of the tree and has no parent so return this rather than 
            // the default parent of the Fluent abstract class.
            base.WhereAny(experssion);
            return this;
        }

        protected QueryExpression getQueryExpression()
        {
            E baseRecord = new E();

            var qryExpression = new QueryExpression(baseRecord.LogicalName);

            qryExpression.ColumnSet = generateColumnSet();
            qryExpression.Criteria = GetFilterExpression();
            return qryExpression;
        }

    }
}
