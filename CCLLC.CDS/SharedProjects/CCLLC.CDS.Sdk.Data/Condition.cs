using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.Sdk
{
    public class Condition<P> : ICondition<P> where P : IFilterable
    {
        public IFilter<P> Parent { get; }
        protected string AttributeName { get; }
        
        public Condition(IFilter<P> parent, string attributeName)
        {
            this.AttributeName = attributeName;
            this.Parent = parent;
        }

        public IFilter<P> Is<T>(ConditionOperator conditionOperator, params T[] values)
        {
            addConditions<T>(conditionOperator, values);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsEqualTo<T>(T[] values)
        {
            addConditions<T>(ConditionOperator.Equal, values);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsGreaterThan<T>(T value)
        {
            addConditionToFilter(ConditionOperator.GreaterThan, value, (IFilter<P>)Parent);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsGreaterThanOrEqualTo<T>(T value)
        {
            addConditionToFilter(ConditionOperator.GreaterEqual, value, Parent);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsLessThan<T>(T value)
        {
            addConditionToFilter(ConditionOperator.LessThan, value, Parent);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsLessThanOrEqualTo<T>(T value)
        {
            addConditionToFilter(ConditionOperator.LessEqual, value, Parent);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsLike(string[] values)
        {
            if (values is null) return (IFilter<P>)Parent;

            //handle wild card conversion
            for(int i=0; i<values.Length; i++)
            {
                if (values[i].Contains("*"))
                {
                    values[i] = values[i].Replace('*', '%');
                }
            }

            addConditions<string>(ConditionOperator.Like, values);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsNotNull()
        {
            addConditionToFilter(ConditionOperator.NotNull, null, Parent);
            return (IFilter<P>)Parent;
        }

        public IFilter<P> IsNull()
        {
            addConditionToFilter(ConditionOperator.Null, null, Parent);
            return (IFilter<P>)Parent;
        }


        private void addConditions<T>(ConditionOperator conditionOperator, T[] values)
        {
            if (values is null) return;

            if (values.Length == 1)
            {
                addConditionToFilter(conditionOperator, values[0], Parent);
                return;
            }

            var impliedOrFilter = new Filter<P>((IFilter<P>)Parent, LogicalOperator.Or);
            foreach(var v in values)
            {
                addConditionToFilter(conditionOperator, v, impliedOrFilter);
            }

            (Parent as IFilter).Filters.Add(impliedOrFilter.ToFilterExpression());
        }

        private void addConditionToFilter(ConditionOperator conditionOperator, object value, IFilterable filter)
        {
            if (value is null)
            {
                filter.Conditions.Add(new ConditionExpression(AttributeName, conditionOperator));
            }
            else
            {
                filter.Conditions.Add(new ConditionExpression(AttributeName, conditionOperator, value));
            }
        }
    }
}
