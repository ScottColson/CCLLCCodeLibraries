using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public interface IExecutableFluentQuery<E> : IFluentQuery<IExecutableFluentQuery<E>, E> where E : Entity
    {
        IList<E> Retreive();

        E FirstOrDefault();
    }
}
