using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;


namespace CCLLC.CDS.Sdk.Utilities.Search
{
    public interface IAliasedSearchQueryBuilder<TParent,TAlias> where TParent : Entity where TAlias : Entity
    {
        IAliasedSearchQueryBuilder<TParent, TAlias> WithLinkingAttribute(string attributeName);

        IAliasedSearchQueryBuilder<TParent, TAlias> WithMappedSearchFields(IDictionary<string, string> mappedFields);

        IAliasedSearchQueryBuilder<TParent, TAlias> AddMappedSearchField(string parentFieldName, string aliasFieldName);

        IAliasedSearchQueryBuilder<TParent, TAlias> AddSearchSignature(ISearchQuerySignature signature);

        QueryExpression Build();        

    }
}
