using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public abstract class Filterable<P> : IFilterable<P> where P : IFilterable
    {
        public IList<FilterExpression> Filters { get; }
        public IList<ConditionExpression> Conditions { get; }
        public virtual IFilterable<P> Parent { get; protected set; }

        public Filterable()
        {
            Filters = new List<FilterExpression>();
            Conditions = new List<ConditionExpression>();           
        }
       

        public virtual P WhereAll(Action<IFilter<P>> expression)
        {
            var filter = new Filter<P>(Parent, LogicalOperator.And);
            expression(filter);
            this.Filters.Add(filter.ToFilterExpression());
            return (P)Parent;
        }

        public virtual P WhereAny(Action<IFilter<P>> experssion)
        {
            var filter = new Filter<P>(Parent, LogicalOperator.Or);
            experssion(filter);
            this.Filters.Add(filter.ToFilterExpression());
            return (P)Parent;
        }

       
    }
}
