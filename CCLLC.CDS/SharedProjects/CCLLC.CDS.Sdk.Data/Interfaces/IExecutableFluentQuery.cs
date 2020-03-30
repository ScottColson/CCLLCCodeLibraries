
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
       public interface IExecutableFluentQuery<E> : IFluentQuery<IExecutableFluentQuery<E>,E> where E : Entity
    {
        IOrganizationService OrganizationService { get; }
        IList<E> Retreive();
    }
}
