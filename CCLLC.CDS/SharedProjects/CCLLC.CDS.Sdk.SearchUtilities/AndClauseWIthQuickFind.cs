using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk.Utilities.Search
{
    public class AndClauseWithQuickFInd : SearchQuerySignatureBase
    {
        public AndClauseWithQuickFInd() : base()
        {
            this.FilterOperator = LogicalOperator.And;
            this.RequireQuickFind = true;
        }
    }
}
