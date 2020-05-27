using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk.Metadata
{
    interface ISdkMessageMetadataService
    {
        IEnumerable<string> GetSdkMessageNames(IOrganizationService orgService, bool includeNonVisibleMessages = false);
        IEnumerable<SdkMessageMetadata> GetSdkMessageMetadata(IOrganizationService orgService, IEnumerable<string> messageNames);
    }
    
}
