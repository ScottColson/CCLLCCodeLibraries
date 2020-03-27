using Microsoft.Xrm.Sdk;


namespace CCLLC.CDS.FluentQuery
{


    public interface IFluentQuery : IQueryEntity
    {
       
    }

    public interface IFluentQuery<P,E> : IFluentQuery, IFilterable<IQueryEntity<IFluentQuery<P,E>,E>> where P : IFluentQuery where E : Entity
    {    
    }
    
}
