using System;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{   
    public interface IFilter : IFilterable
    {
        
    }

    public interface IFilter<P> : IFilterable<P>, IFilter where P : IFilterable
    {
        LogicalOperator Operator { get; }
        IFilter<P> IsActive(bool value = true);

        IFilter<P> HasStatus(params int[] status);

        IFilter<P> HasStatus<T>(params T[] status) where T : Enum;

        ICondition<P> Attribute(string name);

    }
}
