using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public abstract class Filterable<P> : IFilterable<P> where P : IFilterable
    {
        public IList<IFilter> Filters { get; }
        public IList<ConditionExpression> Conditions { get; }
        private P Parent { get; }

        public Filterable(P parent)
        {
            Filters = new List<IFilter>();
            Conditions = new List<ConditionExpression>();
            Parent = parent;
        }
       

        public virtual P WhereAll(Action<IFilter<IFilterable<P>>> experssion)
        {
            var filter = new Filter<IFilterable<P>>(this, LogicalOperator.And);
            experssion(filter);
            this.Filters.Add(filter);
            return Parent;
        }

        public virtual P WhereAny(Action<IFilter<IFilterable<P>>> experssion)
        {
            var filter = new Filter<IFilterable<P>>(this, LogicalOperator.Or);
            experssion(filter);
            this.Filters.Add(filter);
            return Parent;
        }

        public abstract FilterExpression GetFilterExpression();


        protected void AddConditions(ref FilterExpression filterExpression)
        {
            foreach(var c in this.Conditions)
            {
                filterExpression.AddCondition(c);
            }
        }

        protected void AddChildFilters(ref FilterExpression filterExpression)
        {
            foreach(var f in this.Filters)
            {
                filterExpression.AddFilter(f.GetFilterExpression());
            }
        }

        //public virtual FilterExpression Build()
        //{
        //    throw new NotImplementedException();

        //    if(this.Filters.Count == 0)
        //    {
        //        return null; //No Filters
        //    }

        //    if(this.Filters.Count == 1)
        //    {
        //        var filterExpression = new FilterExpression(this.Filters[0].Operator);

        //        foreach(var c in this.Conditions)
        //        return this.Filters[0].Build();
        //    }


        //    var andFilters = this.Filters.Where(f => f.Operator == LogicalOperator.And).ToList();

        //    var orFilters = this.Filters.Where(f => f.Operator == LogicalOperator.Or).ToList();




        //}
    }
}
