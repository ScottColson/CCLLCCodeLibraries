﻿using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Query;
using System.Linq.Expressions;
using System.Linq;

namespace CCLLC.CDS.FluentQuery
{
    public abstract class QueryEntity<P,E> : Filterable<IQueryEntity<P,E>>, IQueryEntity<P,E> where P : IQueryEntity where E : Entity, new()
    {   
        protected IList<IList<string>> Columnsets { get; }
        protected IList<LinkEntity> LinkEntities { get; }
        protected IList<OrderExpression> OrderExpressions { get; }

        private IQueryEntity<P,E> Parent { get; }
        protected string RecordType { get; }

        public QueryEntity(IQueryEntity parent) : base(null)
        {
            RecordType = new E().LogicalName;
            Columnsets = new List<IList<string>>();
            LinkEntities = new List<LinkEntity>();
            OrderExpressions = new List<OrderExpression>();
            this.Parent = this;
        }

        public IQueryEntity<P, E> LeftJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<IQueryEntity<P, E>, RE>> expression) where RE : Entity, new()
        {
            var relatedRecordType = new RE().LogicalName;

            var joinEntity = new JoinedEntity<IQueryEntity<P, E>, RE>(JoinOperator.LeftOuter, RecordType, fromAttributeName, relatedRecordType, toAttributeName, this);
            expression(joinEntity);
            LinkEntities.Add(joinEntity.ToLinkEntity());
            return this;
        }

        public IQueryEntity<P, E> InnerJoin<RE>(string fromAttributeName, string toAttributeName, Action<IJoinedEntity<IQueryEntity<P, E>, RE>> expression) where RE : Entity, new()
        {
            var relatedRecordType = new RE().LogicalName;

            var joinEntity = new JoinedEntity<IQueryEntity<P,E>, RE>(JoinOperator.Inner, RecordType, fromAttributeName, relatedRecordType, toAttributeName, this);
            expression(joinEntity);
            LinkEntities.Add(joinEntity.ToLinkEntity());
            return this;
        }        

        public IQueryEntity<P, E> Select(params string[] columns)
        {
            if(columns != null)
            {
                Columnsets.Add(new List<string>(columns));
            }
            return this;           
        }

        public IQueryEntity<P, E> Select(Expression<Func<E, object>> anonymousTypeInitializer)
        {
            var columns = anonymousTypeInitializer.GetAttributeNamesArray<E>();
                      
            Columnsets.Add(columns);
            return this;
        }

        public IQueryEntity<P, E> SelectAll()
        {
            Columnsets.Add(new List<string>(new string[] { "*" }));
            return this;
        }

        public override FilterExpression GetFilterExpression()
        {
            if (Filters.Count == 0) return null;

            if (Filters.Count == 1) return Filters[0].GetFilterExpression();

            var filterExpression = new FilterExpression(LogicalOperator.And);
            AddChildFilters(ref filterExpression);

            return filterExpression;
        }
           
        protected ColumnSet GetColumnSet()
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

        

        protected bool isSelectAllColumnSet(IList<string> columns)
        {
            return (columns.Where(v => v.Equals("*")).FirstOrDefault() != null);
        }
               

        public IQueryEntity<P, E> OrderByAsc(params string[] columns)
        {
            if(columns != null)
            {
                foreach(var c in columns)
                {
                    OrderExpressions.Add(new OrderExpression(c, OrderType.Ascending));
                }
            }

            return this;
        }

        public IQueryEntity<P, E> OrderByDesc(params string[] columns)
        {
            if (columns != null)
            {
                foreach (var c in columns)
                {
                    OrderExpressions.Add(new OrderExpression(c, OrderType.Descending));
                }
            }

            return this;
        }
    }
}