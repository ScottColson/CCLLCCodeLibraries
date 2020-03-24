using System;

namespace CCLLC.Core
{
    internal class ContainerContract<TContract> : IContainerContract<TContract>, IContractModifiers
    {
        private IocContainer container;
        private bool preexistingRegistration;
        private bool allowOverwrite;
        private bool singleInstance;
        private Type implementation;

        internal ContainerContract(IocContainer container)
        {
            this.container = container;
            this.preexistingRegistration = container.IsRegistered<TContract>();
            this.allowOverwrite = false;
            this.singleInstance = false;
        }

        public IContractModifiers Using<TImplementation>() where TImplementation : TContract
        {
            implementation = typeof(TImplementation);
            register();
            return this;
        }

        public IContractModifiers WithOverwrite()
        {
            this.allowOverwrite = true;
            register();
            return this;
        }

        public IContractModifiers AsSingleInstance()
        {
            this.singleInstance = true;
            register();
            return this;
        }

        private void register()
        {
            if(!preexistingRegistration || allowOverwrite)
            {
                container.RegisterImplementation(typeof(TContract), implementation, singleInstance);
            }
        }
    }
}
