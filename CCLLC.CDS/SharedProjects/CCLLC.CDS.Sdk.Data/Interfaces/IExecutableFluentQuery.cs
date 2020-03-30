using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    public interface IExecutableFluentQuery<E> : IFluentQuery<IExecutableFluentQuery<E>, E> where E : Entity
    {
        /// <summary>
        /// Retrieve records using paging limits.
        /// </summary>
        /// <returns></returns>
        IList<E> Retrieve();

        /// <summary>
        /// Retrieve records from all pages.
        /// </summary>
        /// <returns></returns>
        IList<E> RetrieveAll();

        /// <summary>
        /// Retrieve the first record that matches the query definition.
        /// </summary>
        /// <returns></returns>
        E FirstOrDefault();
    }
}
