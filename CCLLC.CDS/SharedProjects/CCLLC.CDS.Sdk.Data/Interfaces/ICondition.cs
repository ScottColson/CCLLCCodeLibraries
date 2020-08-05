using System.Collections.Generic;

namespace CCLLC.CDS.Sdk
{   
    public interface ICondition<P> where P : IFilterable
    {
        IFilter<P> Parent { get; }
        IFilter<P> Is<T>(Microsoft.Xrm.Sdk.Query.ConditionOperator conditionOperator, params T[] values);
        IFilter<P> IsNull();
        IFilter<P> IsNotNull();
        IFilter<P> IsEqualTo<T>(params T[] values);
        IFilter<P> IsGreaterThanOrEqualTo<T>(T value);
        IFilter<P> IsGreaterThan<T>(T value);
        IFilter<P> IsLessThanOrEqualTo<T>(T value);
        IFilter<P> IsLessThan<T>(T value);
        IFilter<P> IsLike(params string[] values);
        
    }

}
