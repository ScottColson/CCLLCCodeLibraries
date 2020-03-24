using System;

namespace CCLLC.Core
{
    public class SettingsProviderFactory : ISettingsProviderFactory
    {
        private const string CACHE_KEY = "CCLLC.SettingsProviderFactory";
        private const string CACHE_TIMEOUT_SETTING = "CCLLC.SettingsCacheTimeOut";
       
        private ISettingsProviderDataConnector DataConnector { get; }

        public SettingsProviderFactory(ISettingsProviderDataConnector dataConnector)
        {
            DataConnector = dataConnector;
        }

        public ISettingsProvider CreateSettingsProvider(IProcessExecutionContext executionContext, bool useCache = true)
        {
            if(useCache && executionContext.Cache != null && executionContext.Cache.Exists(CACHE_KEY))
            {
                return executionContext.Cache.Get<ISettingsProvider>(CACHE_KEY);
            }

            var settings = DataConnector.LoadSettings(executionContext.DataService);

            var settingsProvider = new SettingsProvider(settings);

            if (useCache)
            {
                var cacheTimeout = settingsProvider.GetValue<TimeSpan?>(CACHE_TIMEOUT_SETTING);

                if (cacheTimeout != null && cacheTimeout.Value.Ticks != 0)
                {
                    executionContext.Cache.Add<ISettingsProvider>(CACHE_KEY, settingsProvider, cacheTimeout.Value);
                }
            }

            return settingsProvider;
        }

    }
}
