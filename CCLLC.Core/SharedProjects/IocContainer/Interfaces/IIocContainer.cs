using System;

namespace CCLLC.Core
{
    /// <summary>
    /// Defines a standard interface for registering interface implementations and resolving
    /// them at runtime.
    /// </summary>
#if IOCBUILD
    public interface IIocContainer : IReadOnlyIocContainer
#else
    internal interface IIocContainer : IReadOnlyIocContainer
#endif
    {      /// <summary>
        /// Register a new implementation contract using fluent registration builder.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        IContainerContract<TContract> Implement<TContract>();  
    }
}
