using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;

namespace CCLLC.CDS.Sdk.Metadata
{
    public interface ISdkMessageMetadataService
    {
        [Obsolete("Obsolete do to performance issues - GetSdkMesageNames(IOrganizationService, IEnumerable<string> messageFilters")]
        IEnumerable<string> GetSdkMessageNames(IOrganizationService orgService, bool includeNonVisibleMessages = false);

        IEnumerable<string> GetSdkMessageNames(IOrganizationService orgService, IEnumerable<string> messageFilters);

        IEnumerable<SdkMessageMetadata> GetSdkMessageMetadata(IOrganizationService orgService, IEnumerable<string> messageNames);
    }
    
}
