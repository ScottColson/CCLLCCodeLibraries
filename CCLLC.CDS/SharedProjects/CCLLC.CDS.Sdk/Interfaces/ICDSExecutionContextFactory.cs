using System;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    /// <summary>
    /// Factor for creating an enhanced CDS execution context based on <see cref="ICDSExecutionContext"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICDSExecutionContextFactory<T> where T :  ICDSExecutionContext
    {
        /// <summary>
        /// Factory create method.
        /// </summary>
        /// <param name="executionContext"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="container"></param>
        /// <param name="runAs"></param>
        /// <returns></returns>
        T CreateCDSExecutionContext(IExecutionContext executionContext, IServiceProvider serviceProvider, IIocContainer container, eRunAs runAs = eRunAs.User);
    }
}
