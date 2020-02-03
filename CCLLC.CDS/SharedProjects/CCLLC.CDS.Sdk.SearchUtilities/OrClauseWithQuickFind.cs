﻿using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk.Utilities.Search
{
    public class OrClauseWithQuickFind : SearchQuerySignatureBase
    {
        public OrClauseWithQuickFind() : base()
        {
            this.FilterOperator = LogicalOperator.Or;
            this.RequireQuickFind = true;
        }
    }
}
