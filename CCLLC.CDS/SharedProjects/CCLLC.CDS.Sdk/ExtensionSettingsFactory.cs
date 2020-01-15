using CCLLC.Core;
using Microsoft.Xrm.Sdk;

using System;

namespace CCLLC.CDS.Sdk
{
    public class ExtensionSettingsFactory : IExtensionSettingsFactory
    {
        protected ICache Cache { get;  }
        protected IEncryptionService Encryption { get; }
        protected IExtensionSettingsConfig Configuration { get; }

       public ExtensionSettingsFactory(ICache cache, IEncryptionService encryption, IExtensionSettingsConfig configuration)
        {
            Cache = cache ?? throw new ArgumentNullException("cache is required.");
            Encryption = encryption ?? throw new ArgumentNullException("encryption is required.");
            Configuration = configuration ?? throw new ArgumentNullException("configuration is required.");
        }

        public ISettingsProvider CreateExtensionSettings(IOrganizationService orgService)
        {
            return new ExtensionSettings(orgService, Cache, Encryption, Configuration);          
        }
    }
}
