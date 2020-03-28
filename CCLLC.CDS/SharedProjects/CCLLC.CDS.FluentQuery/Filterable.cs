using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public abstract class Filterable<P> : IFilterable<P> where P : IFilterable
    {
        public IList<FilterExpression> Filters { get; }
        public IList<ConditionExpression> Conditions { get; }
        private P Parent { get; }

        public Filterable(P parent)
        {
            Filters = new List<FilterExpression>();
            Conditions = new List<ConditionExpression>();
            Parent = parent;
        }
       

        public virtual P WhereAll(Action<IFilter<IFilterable<P>>> experssion)
        {
            var filter = new Filter<IFilterable<P>>(this, LogicalOperator.And);
            experssion(filter);
            this.Filters.Add(filter.ToFilterExpression());
            return Parent;
        }

        public virtual P WhereAny(Action<IFilter<IFilterable<P>>> experssion)
        {
            var filter = new Filter<IFilterable<P>>(this, LogicalOperator.Or);
            experssion(filter);
            this.Filters.Add(filter.ToFilterExpression());
            return Parent;
        }

       // public abstract FilterExpression GetFilterExpression();

       
        
    }
}
