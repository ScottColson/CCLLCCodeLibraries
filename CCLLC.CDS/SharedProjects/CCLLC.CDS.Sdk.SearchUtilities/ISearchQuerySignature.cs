using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk.Utilities.Search
{
    public interface ISearchQuerySignature
    {
        LogicalOperator FilterOperator { get; }
        bool RequireQuickFind { get; }

        bool Test(FilterExpression filter);
    }
}
