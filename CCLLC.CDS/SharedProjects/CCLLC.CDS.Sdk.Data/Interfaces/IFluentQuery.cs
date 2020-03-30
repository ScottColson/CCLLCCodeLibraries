using Microsoft.Xrm.Sdk;


namespace CCLLC.CDS.Sdk
{
    public interface IFluentQuery<P,E> : IQueryEntity<P,E> where P : IFluentQuery<P,E> where E : Entity
    {    
    }
    
}
