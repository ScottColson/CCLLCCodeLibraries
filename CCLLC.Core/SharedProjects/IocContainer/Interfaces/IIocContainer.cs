using System;

namespace CCLLC.Core
{
    /// <summary>
    /// Defines a standard interface for registering interface implementations and resolving
    /// them at runtime.
    /// </summary>
    public interface IIocContainer : IReadOnlyIocContainer
    {
        /// <summary>
        /// Register a new implementation contract using fluent registration buidler.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        IContainerContract<TContract> Implement<TContract>();

        /// <summary>
        /// Register an implementation for a given interface contract.
        /// </summary>
        /// <typeparam name="TContract">The type of the interface contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        [Obsolete("Deprecated method. Use Implement<TContract>().Using<TImplementation>() fluent registration builder which prevents accidental overwrite on an existing implementation. Use .WithOverwrite() to overwrite an existing implementation.")]
        void Register<TContract, TImplementation>() where TImplementation : TContract;

        /// <summary>
        /// Register an single instance implementation for given interface contract. When
        /// an implementation is registers as a single instance, the container creates a
        /// singleton for that implementation so the same object is returned for each
        /// call to <see cref="Resolve{T}"/>.  
        /// </summary>
        /// <typeparam name="TContract">The type of the interface contract.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation.</typeparam>
        [Obsolete("Deprecated method. Use Implement<TContract>().Using<TImplementation>().AsSingleInstance() fluent registration builder which prevents accidental overwrite on an existing implementation. Use .WithOverwrite() to overwrite an existing implementation.")]
        void RegisterAsSingleInstance<TContract, TImplementation>() where TImplementation : TContract;
                
    }
}
