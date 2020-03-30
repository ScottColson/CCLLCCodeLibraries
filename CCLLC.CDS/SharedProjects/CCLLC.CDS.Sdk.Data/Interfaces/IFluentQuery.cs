using Microsoft.Xrm.Sdk;


namespace CCLLC.CDS.FluentQuery
{
    public interface IFluentQuery<P,E> : IQueryEntity<P,E> where P : IFluentQuery<P,E> where E : Entity
    {    
    }
    
}
