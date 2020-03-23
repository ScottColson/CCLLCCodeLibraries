namespace CCLLC.Core
{
    public interface IContainerContract<TContract>
    {
        /// <summary>
        /// Specify the concrete implementation for the contract interface.
        /// </summary>         
        IContractModifiers Using<TImplementation>() where TImplementation : TContract;
    }
}
