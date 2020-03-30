using Microsoft.Xrm.Sdk;


namespace CCLLC.CDS.Sdk
{
    public interface IFluentQuery { }

    public interface IFluentQuery<P,E> : IFluentQuery, IQueryEntity<P,E> where P : IFluentQuery<P,E> where E : Entity
    {    
        IFluentQuerySettings<P,E> With { get; }
    }

    public interface IFluentQuerySettings<P,E> where P : IFluentQuery<P,E> where E : Entity
    { 
        /// <summary>
        /// Turn database record locking on or off. Record locking is off by default.
        /// </summary>
        /// <param name="useLock"></param>
        /// <returns></returns>
        P DatabaseLock(bool useLock = true);

        /// <summary>
        /// Limits the number of records that will be retrieved.
        /// </summary>
        /// <param name="recordLimit"></param>
        /// <returns></returns>
        P RecordLimit(int? recordLimit);

        /// <summary>
        /// Limits the results to unique records when set to true. 
        /// </summary>
        /// <param name="uniqueRecords"></param>
        /// <returns></returns>
        P UniqueRecords(bool uniqueRecords = true);
    }
    
}
