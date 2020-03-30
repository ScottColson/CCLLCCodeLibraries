﻿using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;

namespace CCLLC.CDS.Sdk
{
    public abstract class FluentQuery<P, E> : QueryEntity<P, E>, IFluentQuery<P, E> where P : IFluentQuery<P, E> where E : Entity, new()
    {
        class FluentQuerySettings<P,E> : IFluentQuerySettings<P,E> where P : IFluentQuery<P, E> where E : Entity
        {
            private IFluentQuery Parent { get; }
            public bool UseNoLock { get; private set; }
            public int? TopCount { get; private set; }
            public bool Distinct { get; private set; }

            public FluentQuerySettings(IFluentQuery parent)
            {
                Parent = parent;
                UseNoLock = true;
            }

            public P DatabaseLock(bool useLock = true)
            {
                UseNoLock = !useLock;
                return (P)Parent;
            }

            public P RecordLimit(int? recordLimit)
            {
                TopCount = recordLimit;
                return (P)Parent;
            }

            public P UniqueRecords(bool uniqueRecords = true)
            {
                Distinct = uniqueRecords;
                return (P)Parent;
            }
        }

        private FluentQuerySettings<P,E> Settings { get; }

        public FluentQuery() : base()
        {
            Settings = new FluentQuerySettings<P, E>(this);
        }

        public IFluentQuerySettings<P, E> With => Settings;

        protected QueryExpression getQueryExpression()
        {
            E baseRecord = new E();

            var qryExpression = new QueryExpression(baseRecord.LogicalName);
           
            qryExpression.NoLock = Settings.UseNoLock;
            qryExpression.TopCount = Settings.TopCount;
            qryExpression.Distinct = Settings.Distinct;
            qryExpression.ColumnSet = GetColumnSet();
            qryExpression.Criteria = GetFilterExpression();
            qryExpression.LinkEntities.AddRange(LinkEntities);
            qryExpression.Orders.AddRange(OrderExpressions);

            return qryExpression;
        }


        
    }
}
