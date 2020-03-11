using System.Collections.Generic;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public class SettingsDataConnector : ISettingsProviderDataConnector
    {
        private IEncryptionService EncryptionService { get; }
        private readonly string EncryptionKey = "7a5a64brEgaceqenuyegac7era3Ape6aWatrewegeka94waqegayathudrebuc7t";

        public SettingsDataConnector(IEncryptionService encryptionService)
        {
            EncryptionService = encryptionService;
        }

        public IReadOnlyDictionary<string,string> LoadSettings(IDataService dataService)
        {
            var orgService = dataService.ToOrgService();

            //query the system for all active extension settings
            var qry = new QueryByAttribute("ccllc_extensionsettings");
            qry.ColumnSet = new ColumnSet(new string[] { "ccllc_name", "ccllc_value", "ccllc_encryptedflag" });
            qry.AddAttributeValue("statecode", 0);

            var result = orgService.RetrieveMultiple(qry);
            Dictionary<string, string> entries = new Dictionary<string, string>(result.Entities.Count);

            foreach (var setting in result.Entities)
            {
                var name = setting.GetAttributeValue<string>("ccllc_name");                
                var value = setting.GetAttributeValue<string>("ccllc_value");

                bool encrypted = setting.GetAttributeValue<bool>("ccllc_encryptedflag");

                if (encrypted)
                {
                    value = EncryptionService.Decrypt(value, EncryptionKey);
                }
                entries.Add(name, value);
            }

            return entries;
        }
    }
}
