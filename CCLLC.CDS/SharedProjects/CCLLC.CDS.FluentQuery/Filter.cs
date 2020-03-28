using System;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public partial class Filter<P> : Filterable<P>, IFilter<P> where P : IFilterable
    {
        public LogicalOperator Operator { get; }      
        private P Parent { get; }

        public Filter(P parent, LogicalOperator logicalOperator) : base(parent)
        {
            this.Parent = parent;
            this.Operator = logicalOperator;           
        }

        public IFilter<P> IsActive(bool value = true)
        {
            Conditions.Add(new ConditionExpression("statecode", ConditionOperator.Equal, (value == true) ? 0:1));
            return this;
        }

        public IFilter<P> HasStatus(params int[] status)
        {
            if(status != null)
            {
                if(status.Length == 1)
                {
                    Conditions.Add(new ConditionExpression("statuscode", ConditionOperator.Equal, status[0]));
                }
                else
                {
                    //Multiple status values imply an OR clause for each value.
                    var statusFilter = new Filter<IFilter<P>>(this, LogicalOperator.Or);
                    foreach(var s in status)
                    {
                        statusFilter.Conditions.Add(new ConditionExpression("statuscode", ConditionOperator.Equal, s));
                    }
                    this.Filters.Add(statusFilter.ToFilterExpression());
                }
            }
            return this;
        }

        public IFilter<P> HasStatus<T>(params T[] status) where T : Enum
        {
            if(status != null && status.Length > 0)
            {
                var statusAsInt =  Array.ConvertAll(status, value => (int)(object)value);
                return HasStatus(statusAsInt);
            }
            return this;            
        }

        public ICondition<IFilter<P>> Attribute(string name)
        {
            return new Condition<IFilter<P>> (this, name);
        }

        public FilterExpression ToFilterExpression()
        {
            var filterExpression = new FilterExpression(Operator);
            filterExpression.Conditions.AddRange(Conditions);
            filterExpression.Filters.AddRange(Filters);
            
            return filterExpression;            
        }
       
    }

}
