using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.FluentQuery
{  
    public interface IJoinedEntity<P,E,RE> : IQueryEntity, IQueryEntity<IJoinedEntity<P,E,RE>,RE> where P : IQueryEntity<P,E> where E : Entity where RE : Entity
    {
        IJoinedEntity<P, E, RE> WithAlias(string aliasName);

        LinkEntity ToLinkEntity();
    }
}
