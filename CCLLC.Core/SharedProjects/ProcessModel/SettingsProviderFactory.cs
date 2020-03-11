using System;

namespace CCLLC.Core
{
    public class SettingsProviderFactory : ISettingsProviderFactory
    {
        private const string CACHE_KEY = "CCLLC.SettingsProviderFactory";
        private const string CACHE_TIMEOUT_SETTING = "CCLLC.SettingsCacheTimeOut";
        private TimeSpan DEFAULT_TIMEOUT = TimeSpan.FromMinutes(15);

        private ISettingsProviderDataConnector DataConnector { get; }

        public SettingsProviderFactory(ISettingsProviderDataConnector dataConnector)
        {
            DataConnector = dataConnector;
        }

        public ISettingsProvider CreateSettingsProvider(IProcessExecutionContext executionContext, TimeSpan? overrideCacheTimeout = null)
        {
            if(executionContext.Cache != null && executionContext.Cache.Exists(CACHE_KEY))
            {
                return executionContext.Cache.Get<ISettingsProvider>(CACHE_KEY);
            }

            var settings = DataConnector.LoadSettings(executionContext.DataService);

            var settingsProvider = new SettingsProvider(settings);

            var cacheTimeout = (overrideCacheTimeout != null) 
                ? overrideCacheTimeout.Value 
                : settingsProvider.GetValue<TimeSpan?>(CACHE_TIMEOUT_SETTING, DEFAULT_TIMEOUT);

            if(cacheTimeout != null)
            {
                executionContext.Cache.Add<ISettingsProvider>(CACHE_KEY, settingsProvider, cacheTimeout.Value);
            }

            return settingsProvider;
        }

    }
}
