using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public interface IFilterable
    {
        IList<FilterExpression> Filters { get; }
        IList<ConditionExpression> Conditions { get; }
    }


    public interface IFilterable<P> : IFilterable where P : IFilterable
    {
        P WhereAll(Action<IFilter<IFilterable<P>>> experssion);
        
        /// <summary>
        /// Evaluate contained conditions using a logical OR.
        /// </summary>
        /// <param name="experssion"></param>
        /// <returns></returns>
        P WhereAny(Action<IFilter<IFilterable<P>>> experssion);
        
    }
}
