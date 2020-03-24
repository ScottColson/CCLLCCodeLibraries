using System.Collections.Generic;

namespace CCLLC.Core
{
    public interface ISettingsProviderDataConnector
    {
        IReadOnlyDictionary<string, string> LoadSettings(IDataService dataService);
    }
}
