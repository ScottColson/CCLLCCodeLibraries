using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.FluentQuery
{
    public class Condition<P> : ICondition<P> where P : IFilter
    {
        protected P Parent { get; }
        protected string AttributeName { get; }

        public Condition(P parent, string attributeName)
        {
            this.AttributeName = attributeName;
            this.Parent = parent;
        }

        public P Is<T>(ConditionOperator conditionOperator, params T[] values)
        {            
            throw new NotImplementedException();
        }

        public P IsEqualTo<T>(T[] values)
        {
            addConditions<T>(ConditionOperator.Equal, values);
            return Parent;
        }

        public P IsGreaterThan<T>(T value)
        {
            addConditionToFilter(ConditionOperator.GreaterThan, value, Parent);
            return Parent;
        }

        public P IsGreaterThanOrEqualTo<T>(T value)
        {
            addConditionToFilter(ConditionOperator.GreaterEqual, value, Parent);
            return Parent;
        }

        public P IsLessThan<T>(T value)
        {
            addConditionToFilter(ConditionOperator.LessThan, value, Parent);
            return Parent;
        }

        public P IsLessThanOrEqualTo<T>(T value)
        {
            addConditionToFilter(ConditionOperator.LessEqual, value, Parent);
            return Parent;
        }

        public P IsNotNull()
        {
            addConditionToFilter(ConditionOperator.NotNull, null, Parent);
            return Parent;
        }

        public P IsNull()
        {
            addConditionToFilter(ConditionOperator.Null, null, Parent);
            return Parent;
        }


        private void addConditions<T>(ConditionOperator conditionOperator, T[] values)
        {
            if (values is null) return;

            if (values.Length == 1)
            {
                addConditionToFilter(conditionOperator, values, Parent);
                return;
            }

            var impliedOrFilter = new Filter<P>(Parent, LogicalOperator.Or);
            foreach(var v in values)
            {
                addConditionToFilter(conditionOperator, v, impliedOrFilter);
            }

            Parent.Filters.Add(impliedOrFilter);
        }

        private void addConditionToFilter(ConditionOperator conditionOperator, object value, IFilter filter)
        {            
            filter.Conditions.Add(new ConditionExpression(AttributeName, conditionOperator, value));
        }
    }
}
