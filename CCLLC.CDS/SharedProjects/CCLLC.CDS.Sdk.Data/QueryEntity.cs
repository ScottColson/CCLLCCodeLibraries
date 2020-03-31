using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Query;
using System.Linq.Expressions;
using System.Linq;

namespace CCLLC.CDS.Sdk
{
    public abstract class QueryEntity<P,E> : Filterable<P>, IQueryEntity<P,E> where P : IQueryEntity<P,E> where E : Entity, new()
    {   
        protected IList<IList<string>> Columnsets { get; }
        protected IList<LinkEntity> LinkEntities { get; }
        protected IList<OrderExpression> OrderExpressions { get; }
                
        protected string RecordType { get; }

        public QueryEntity() : base()
        {
            RecordType = new E().LogicalName;
            Columnsets = new List<IList<string>>();
            LinkEntities = new List<LinkEntity>();
            OrderExpressions = new List<OrderExpression>();
            this.Parent = this;
        }

        public P LeftJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<P, E, RE>> expression) where RE : Entity, new()
        {
            var relatedRecordType = new RE().LogicalName;

            var joinEntity = new JoinedEntity<P,E, RE>(JoinOperator.LeftOuter, RecordType, fromAttributeName, relatedRecordType, toAttributeName, this);
            expression(joinEntity);
            LinkEntities.Add(joinEntity.ToLinkEntity());
            return (P)Parent;
        }

       

        public P InnerJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<P, E, RE>> expression) where RE : Entity, new()
        {
            var relatedRecordType = new RE().LogicalName;

            var joinEntity = new JoinedEntity<P, E ,RE>(JoinOperator.Inner, RecordType, fromAttributeName, relatedRecordType, toAttributeName, this);
            expression(joinEntity);
            LinkEntities.Add(joinEntity.ToLinkEntity());
            return (P)Parent;
        }        

        public P Select(params string[] columns)
        {
            if(columns != null)
            {
                Columnsets.Add(new List<string>(columns));
            }
            return (P)Parent;          
        }

        public P Select(Expression<Func<E, object>> anonymousTypeInitializer)
        {
            var columns = anonymousTypeInitializer.GetAttributeNamesArray<E>();
                      
            Columnsets.Add(columns);
            return (P)Parent;
        }

        public P SelectAll()
        {
            Columnsets.Add(new List<string>(new string[] { "*" }));
            return (P)Parent;
        }

        public P OrderByAsc(params string[] columns)
        {
            if (columns != null)
            {
                foreach (var c in columns)
                {
                    OrderExpressions.Add(new OrderExpression(c, OrderType.Ascending));
                }
            }

            return (P)Parent;
        }

        public P OrderByDesc(params string[] columns)
        {
            if (columns != null)
            {
                foreach (var c in columns)
                {
                    OrderExpressions.Add(new OrderExpression(c, OrderType.Descending));
                }
            }

            return (P)Parent;
        }

        protected FilterExpression GetFilterExpression()
        {
            if (Filters.Count == 0) return new FilterExpression();

            if (Filters.Count == 1) return Filters[0];

            // Wrap multiple filters in an AND filter.
            var filterExpression = new FilterExpression(LogicalOperator.And);
            filterExpression.Filters.AddRange(Filters);

            return filterExpression;
        }
           
        protected virtual ColumnSet GetColumnSet()
        {
            var uniqueColumns = new List<string>();
                      

            foreach (var cs in this.Columnsets)
            {
                if (isSelectAllColumnSet(cs))
                    return new ColumnSet(true);

                foreach (var c in cs)
                {
                    if (!uniqueColumns.Contains(c))
                    {
                        uniqueColumns.Add(c);
                    }
                }
            }

            return new ColumnSet(uniqueColumns.ToArray());
        }
           
        private bool isSelectAllColumnSet(IList<string> columns)
        {
            return (columns.Where(v => v.Equals("*")).FirstOrDefault() != null);
        }

       

       
    }
}
