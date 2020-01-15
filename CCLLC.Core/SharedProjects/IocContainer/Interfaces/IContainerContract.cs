﻿namespace CCLLC.Core
{
    public interface IContainerContract<TContract>
    {
        /// <summary>
        /// Specify the concreate implementation for the contract interface.
        /// </summary>         
        IContractModifiers Using<TImplementation>() where TImplementation : TContract;
    }
}
