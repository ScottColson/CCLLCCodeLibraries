using System;
using System.Collections;
using System.Collections.Generic;

namespace CCLLC.Core
{
    public class SettingsProvider : ISettingsProvider
    {
        //Possible separators for defining an array of strings as a value.
        private readonly char[] SEPARATORS = { ';', ',' };

        private IDictionary<string,string> Settings { get; }

        public SettingsProvider(IReadOnlyDictionary<string,string> settings)
        {
            Settings = new Dictionary<string, string>();

            if(settings != null)
            {
                foreach(var setting in settings)
                {
                    Settings.Add(setting.Key.ToLower(),setting.Value);
                }     
            }           
        }
        
        public string this[string key] => Settings[key];

        public IEnumerable<string> Keys => Settings.Keys;

        public IEnumerable<string> Values => Settings.Values;

        public int Count => Settings.Count;

        public bool ContainsKey(string key)
        {
            return Settings.ContainsKey(key);
        }

        /// <summary>
        /// Get the setting value identified by the key. If setting is not available return the optional supplied default value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetValue<T>(string key, T defaultValue = default)
        {
            string value = null;
            if (TryGetValue(key.ToLower(), out value))
            {
                //handle string array conversion
                if (typeof(T) == typeof(string[]))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        return (T)((object)new string[0]);
                    }

                    //remove any white space following the semicolon or comma separators in the list.
                    value = System.Text.RegularExpressions.Regex.Replace(value, @";\s+", ";");
                    value = System.Text.RegularExpressions.Regex.Replace(value, @",\s+", ",");

                    //split the value string into an array
                    var array = value.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);

                    return (T)((object)array);
                }

                //handle TimeSpan? conversion
                if (typeof(T) == typeof(TimeSpan?))
                {
                    int timeInSeconds = -1;
                    if (int.TryParse(value, out timeInSeconds))
                    {
                        if (timeInSeconds >= 0)
                        {
                            return (T)((object)TimeSpan.FromSeconds(timeInSeconds));
                        }
                    }

                    return (T)((object)null);
                }

                //handle all other conversion attempts.
                return (T)((object)Convert.ChangeType(value, typeof(T)));
            }

            return defaultValue;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Settings.GetEnumerator();
        }

        public bool TryGetValue(string key, out string value)
        {
            return Settings.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Settings.GetEnumerator();
        }
    }
}
