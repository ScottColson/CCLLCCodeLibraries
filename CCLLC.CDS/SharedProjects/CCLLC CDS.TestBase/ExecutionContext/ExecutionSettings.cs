using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CCLLC.Core;

namespace CCLLC.CDS.Test.ExecutionContext
{
    public class FakeExecutionSettings : ISettingsProvider
    {
        char[] SEPARATORS = { ';', ',' };

        private IDictionary<string, string> Settings;

        public FakeExecutionSettings()
            :this(new Dictionary<string, string>())
        { }

        public FakeExecutionSettings(IDictionary<string,string> settings)
        {
            Settings = settings ?? throw new ArgumentNullException("settings");
        }

        public string this[string key] => Settings[key];

        public IEnumerable<string> Keys => Settings.Keys;

        public IEnumerable<string> Values => Settings.Values;

        public int Count => Settings.Count;

        public bool ContainsKey(string key)
        {
            return Settings.ContainsKey(key);
        }

        public T GetValue<T>(string Key, T DefaultValue = default(T))
        {
            string value = null;
            if (TryGetValue(Key.ToLower(), out value))
            {
                //handle string array conversion
                if (typeof(T) == typeof(string[]))
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        return (T)((object)new string[0]);
                    }

                    //remove any white space following the semicolon or comma separators in the list.
                    value = Regex.Replace(value, @";\s+", ";");
                    value = Regex.Replace(value, @",\s+", ",");

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
                        if (timeInSeconds > 0)
                        {
                            return (T)((object)TimeSpan.FromSeconds(timeInSeconds));
                        }
                    }

                    return (T)((object)null);
                }

                //handle all other conversion attempts.
                return (T)((object)Convert.ChangeType(value, typeof(T)));
            }

            return DefaultValue;
        }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
        {
            return Settings.GetEnumerator();
        }
                

        public bool TryGetValue(string key, out string value)
        {
            return TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Settings.GetEnumerator();
        }
    }
}
