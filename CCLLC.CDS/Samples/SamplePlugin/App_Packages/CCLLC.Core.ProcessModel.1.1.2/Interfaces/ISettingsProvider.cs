using System.Collections.Generic;

namespace CCLLC.Core
{
    /// <summary>
    /// Settings provider based on a simple dictionary of string values.
    /// </summary>
    public interface ISettingsProvider : IReadOnlyDictionary<string,string>
    {
        /// <summary>
        /// Retrieve a type converted setting from the provider. Return an optional default value if
        /// the setting is not in the dictionary.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetValue<T>(string key, T defaultValue = default(T));
    }
}
