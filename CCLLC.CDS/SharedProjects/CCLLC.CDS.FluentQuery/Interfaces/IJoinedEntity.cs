using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.FluentQuery
{  

    public interface IJoinedEntity : IQueryEntity{}

    public interface IJoinedEntity<P,E> : IJoinedEntity, IQueryEntity<P,E> where P : IQueryEntity where E : Entity
    {
        IJoinedEntity<P, E> WithAlias(string aliasName);
    }
}
