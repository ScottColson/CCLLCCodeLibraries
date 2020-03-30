using System;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    public partial class Filter<P> : Filterable<P>, IFilter<P> where P : IFilterable
    {
        public LogicalOperator Operator { get; }      
     
        public Filter(IFilterable<P> parent, LogicalOperator logicalOperator) : base()
        {
            this.Parent = parent;
            this.Operator = logicalOperator;           
        }

        public IFilter<P> IsActive(bool value = true)
        {
            Conditions.Add(new ConditionExpression("statecode", ConditionOperator.Equal, (value == true) ? 0:1));
            return (IFilter<P>)this;
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
                    var statusFilter = new Filter<P>(this, LogicalOperator.Or);
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

       

        public FilterExpression ToFilterExpression()
        {
            var filterExpression = new FilterExpression(Operator);
            filterExpression.Conditions.AddRange(Conditions);
            filterExpression.Filters.AddRange(Filters);
            
            return filterExpression;            
        }

        public ICondition<P> Attribute(string name)
        {
            return new Condition<P>(this, name);
            throw new NotImplementedException();
        }
    }

}
