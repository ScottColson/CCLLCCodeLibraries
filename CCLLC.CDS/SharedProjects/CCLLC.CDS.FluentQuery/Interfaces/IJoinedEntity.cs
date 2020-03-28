using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{  

    public interface IJoinedEntity : IQueryEntity
    {
        LinkEntity ToLinkEntity();
    }

    public interface IJoinedEntity<P,E> : IJoinedEntity, IQueryEntity<P,E> where P : IQueryEntity where E : Entity
    {
        IJoinedEntity<P, E> WithAlias(string aliasName);
    }
}
