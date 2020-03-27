
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.FluentQuery
{
    public interface IExecutableFluentQuery : IFluentQuery 
    {
        IOrganizationService OrganizationService { get; }
    }

    public interface IExecutableFluentQuery<E> : IExecutableFluentQuery, IFluentQuery<IExecutableFluentQuery,E> where E : Entity
    {
        
        IList<E> Retreive();
    }
}
