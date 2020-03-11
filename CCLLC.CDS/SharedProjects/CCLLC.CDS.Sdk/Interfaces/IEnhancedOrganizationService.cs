using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    /// <summary>
    /// Defines an enhanced organization service that is compatible with the <see cref="IOrganizationService"/> provided
    /// by the Microsoft.Xrm.Sdk and the <see cref="IDataService"/> provided by CCLLC.Core.ProcessModel. This allows the enhanced
    /// service to pass through business logic layers that are data service agnostic.
    /// </summary>
    public interface IEnhancedOrganizationService : IOrganizationService, IDataService
    {
    }
}
