namespace CCLLC.CDS.FluentQuery
{
    public interface ICondition { }

    public interface ICondition<P> : ICondition where P : IFilter
    {
        P Is<T>(Microsoft.Xrm.Sdk.Query.ConditionOperator conditionOperator, params T[] values);
        P IsNull();
        P IsNotNull();
        P IsEqualTo<T>(params T[] values);
        P IsGreaterThanOrEqualTo<T>(T value);
        P IsGreaterThan<T>(T value);
        P IsLessThanOrEqualTo<T>(T value);
        P IsLessThan<T>(T value);
    }

}
