using System;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{
    public interface IFilter : IFilterable
    {   
        LogicalOperator Operator { get; }
    }

    public interface IFilter<P> : IFilter, IFilterable<P> where P : IFilterable
    {
        IFilter<P> IsActive(bool value = true);

        IFilter<P> HasStatus(params int[] status);

        IFilter<P> HasStatus<T>(params T[] status) where T : Enum;

        ICondition<IFilter<P>> Attribute(string name);

    }
}
