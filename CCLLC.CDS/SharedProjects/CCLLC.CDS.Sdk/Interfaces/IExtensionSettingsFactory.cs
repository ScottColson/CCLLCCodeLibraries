using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public interface IExtensionSettingsFactory
    {
        ISettingsProvider CreateExtensionSettings(IOrganizationService orgService);
    }
}
