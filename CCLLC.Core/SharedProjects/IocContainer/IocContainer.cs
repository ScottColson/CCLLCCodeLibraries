using System;
using System.Collections.Generic;
using System.Reflection;

namespace CCLLC.Core
{
    /// <summary>
    /// Provides a light weight IOC container implementation for configuring services 
    /// Based on work from Ken Egozi: http://kenegozi.com/blog/2008/01/17/its-my-turn-to-build-an-ioc-container-in-15-minutes-and-33-lines
    /// </summary>
    public class IocContainer : IIocContainer

    {        
        private readonly IDictionary<Type, ContractModifiers> registeredTypes = new Dictionary<Type, ContractModifiers>();
        private readonly IDictionary<Type, object> instances = new Dictionary<Type, object>();        
        
        /// <summary>
        /// The number of items registered in the container.
        /// </summary>
        public int Count => registeredTypes.Count;

        /// <summary>
        /// Register 
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns></returns>
        public IContainerContract<TContract> Implement<TContract>()
        {
            IContainerContract<TContract> contract = new ContainerContract<TContract>(this);
            return contract;
        }

        /// <summary>
        /// Return an implementation for the desired contract interface.
        /// </summary>
        /// <typeparam name="T">The type of the interface contract.</typeparam>
        /// <returns>The registered implementation as the requested interface contract.</returns>
        public T Resolve<T>()
        {
            return (T)resolveContract(typeof(T));
        }

        private object resolveContract(Type contract)
        {
            if (!isRegistered(contract))
            {
                throw new Exception(string.Format("Type {0} is not registered in the container.", contract.ToString()));
            }

            var implementation = registeredTypes[contract];

            if (implementation.SingleInstance && instances.ContainsKey(implementation.Type))
            {
                return instances[implementation.Type];
            }            

            ConstructorInfo constructor = implementation.Type.GetConstructors()[0];
            ParameterInfo[] constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
            {
                var instance = Activator.CreateInstance(implementation.Type);
                if (implementation.SingleInstance)
                {
                    instances[implementation.Type] = instance;
                }
                return instance;
            }
            else
            {
                List<object> parameters = new List<object>(constructorParameters.Length);
                foreach (ParameterInfo parameterInfo in constructorParameters)
                {
                    parameters.Add(resolveContract(parameterInfo.ParameterType));
                }

                var instance = constructor.Invoke(parameters.ToArray());
                if (implementation.SingleInstance)
                {
                    instances[implementation.Type] = instance;
                }
                return instance;
            }
        }

        /// <summary>
        /// Check to see if a given contract is already registered in the container.
        /// </summary>
        /// <typeparam name="TContract"></typeparam>
        /// <returns>Returns true if the interface contract is already registered.</returns>
        public bool IsRegistered<TContract>()
        {
            return isRegistered(typeof(TContract));
        }

        private bool isRegistered(Type contract)
        {
            return registeredTypes.ContainsKey(contract);
        }

        internal void RegisterImplementation(Type contract, Type implementation, bool singleInstance)
        {
            var implementationParam = new ContractModifiers { Type = implementation, SingleInstance = singleInstance };
            registeredTypes[contract] = implementationParam;

            //if this new implementation has been rendered and cached as a single instance then clear that instance now.
            if (instances.ContainsKey(implementation))
            {
                instances.Remove(implementation);
            }
        }       
    }
}
