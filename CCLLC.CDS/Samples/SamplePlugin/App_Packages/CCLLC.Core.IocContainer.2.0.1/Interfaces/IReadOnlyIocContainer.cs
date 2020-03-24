using System;

namespace CCLLC.Core
{
    public interface IReadOnlyIocContainer
    {
        /// <summary>
        /// Return an implementation for the desired contract interface.
        /// </summary>
        /// <typeparam name="T">The type of the interface contract.</typeparam>
        /// <returns>The registered implementation as the requested interface contract.</returns>
        T Resolve<T>();

        /// <summary>
        /// Check to see if a given contract is already registered in the container.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns>Returns true if the interface contract is already registered.</returns>
        bool IsRegistered<TContract>();

        /// <summary>
        /// The number of items registered in the container.
        /// </summary>
        int Count { get; }
    }
}
