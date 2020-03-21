using System;

namespace CCLLC.Core
{
    /// <summary>
    /// Factory to create <see cref="ISettingsProvider"/>.
    /// </summary>
    public interface ISettingsProviderFactory
    {
        /// <summary>
        /// Create a new <see cref="ISettingsProvider"/> object.
        /// </summary>
        /// <param name="executionContext"></param>
        /// <param name="overrideCacheTimeout"></param>
        /// <returns></returns>
        ISettingsProvider CreateSettingsProvider(IProcessExecutionContext executionContext, bool useCache = true);
    }
}
