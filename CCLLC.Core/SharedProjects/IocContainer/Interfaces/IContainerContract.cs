namespace CCLLC.Core
{
#if IOCBUILD
    public interface IContainerContract<TContract>
#else     
    internal interface IContainerContract<TContract>
#endif
    {
        /// <summary>
        /// Specify the concrete implementation for the contract interface.
        /// </summary>         
        IContractModifiers Using<TImplementation>() where TImplementation : TContract;
    }
}
