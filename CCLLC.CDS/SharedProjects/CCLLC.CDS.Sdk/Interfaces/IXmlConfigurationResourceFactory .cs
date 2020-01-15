using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk
{    
    using CCLLC.Core;

   
    public interface IXmlConfigurationResourceFactory
    {
       
        IXmlConfigurationResource CreateConfigurationResources(IOrganizationService orgService);
    }
}
