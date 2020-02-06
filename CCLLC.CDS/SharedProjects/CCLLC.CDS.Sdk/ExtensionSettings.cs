using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CCLLC.CDS.Sdk
{
    using CCLLC.Core;

    public class ExtensionSettings : ISettingsProvider
    {        
        const int MAX_CACHE_TIMEOUT = 28800; //8 hours
        const int MIN_CACHE_TIMEOUT = 0; //no cache

        const string CACHE_ENTRIES_KEY = "CCLLC.CDS.ExtensionSettings.Entries.Dictionary";
        const string CACHE_SETTING_NAME = "CacheTimeOut";
        const string DEFAULT_ENCRYPTION_KEY = "7a5a64brEgaceqenuyegac7era3Ape6aWatrewegeka94waqegayathudrebuc7t";

        private static object syncRoot = new Object();

        private IOrganizationService orgService;
        private ICache cache;
        private IEncryptionService encryption;
        private IExtensionSettingsConfig config;
        private string encryptionKey;

        char[] SEPARATORS = { ';', ',' };

        public ExtensionSettings(IOrganizationService OrgService, ICache cache, IEncryptionService encryption, IExtensionSettingsConfig config)
        {
            try
            {
                if (OrgService == null) { throw new ArgumentNullException("OrgService is required."); }
                if (cache == null) { throw new ArgumentNullException("cache is required."); }
                if (config == null) { throw new ArgumentNullException("config is required."); }
                if (encryption == null) { throw new ArgumentNullException("encryption is required."); }
                this.config = config;
                this.orgService = OrgService;
                this.cache = cache;
                this.encryption = encryption;
                encryptionKey = !string.IsNullOrEmpty(config.EncryptionKey) ? config.EncryptionKey : DEFAULT_ENCRYPTION_KEY;
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("Error constructing ExtensionSettings: {0}", ex.Message), ex);
            }
        }

        private int GetCacheTimeout(Dictionary<string, string> entries)
        {
            //Check settings for custom cache timeout setting otherwise use the default
            int expireSeconds;
            string val;

            if (entries.TryGetValue(CACHE_SETTING_NAME.ToLower(), out val))
            {
                expireSeconds = System.Convert.ToInt32(val);
                if (expireSeconds > MAX_CACHE_TIMEOUT)
                {
                    expireSeconds = MAX_CACHE_TIMEOUT;
                }
                else if (expireSeconds < MIN_CACHE_TIMEOUT)
                {
                    expireSeconds = MIN_CACHE_TIMEOUT;
                }
            }
            else
                expireSeconds = config.DefaultTimeout;

            return expireSeconds;
        }

        private Dictionary<string, string> RetrieveCurrentSettings()
        {
            //lock access while refresh is being completed.
            lock (syncRoot)
            {
                //reload the cache
                try
                {
                    if (this.orgService == null) { throw new Exception("OrganizationService is null."); }
                    if (this.config == null) { throw new Exception("ExtensionSettingsConfig is null."); }

                    //query the system for all active extension settings
                    var qry = new QueryByAttribute(config.EntityName);
                    qry.ColumnSet = new ColumnSet(new string[] { config.NameColumn, config.ValueColumn, config.EncryptionColumn });
                    qry.AddAttributeValue("statecode", 0);

                    var result = this.orgService.RetrieveMultiple(qry);
                    Dictionary<string, string> entries = new Dictionary<string, string>(result.Entities.Count);

                    foreach (Entity setting in result.Entities)
                    {
                        var name = setting.GetAttributeValue<string>(config.NameColumn);
                        name = !string.IsNullOrEmpty(name) ? name.ToLowerInvariant() : string.Empty;
                        var value = setting.GetAttributeValue<string>(config.ValueColumn);

                        bool encrypted = setting.GetAttributeValue<bool>(config.EncryptionColumn);

                        if (encrypted)
                        {
                            value = encryption.Decrypt(value, encryptionKey);
                        }
                        entries.Add(name, value);
                    }
                    return entries;
                }
                catch (Exception ex)
                {
                    throw new Exception(string.Format("Error retrieving extension settings: {0}", ex.Message));
                }

            } //end lock
        }

        public T Get<T>(string Key, T DefaultValue = default(T))
        {
            string value;
            var entries = cache.Get(CACHE_ENTRIES_KEY) as Dictionary<string, string>;
            if (entries == null)
            {
                entries = RetrieveCurrentSettings();
                if (entries == null)
                    throw new Exception("The cache is empty after being refreshed");
                int seconds = GetCacheTimeout(entries);
                cache.Add(CACHE_ENTRIES_KEY, entries, seconds);
            }

            if (entries.TryGetValue(Key.ToLower(), out value))
            {
                if (typeof(T) != typeof(string[]))
                    return (T)((object)Convert.ChangeType(value, typeof(T)));
                else
                {
                    //create a string array to return
                    string[] strArray = new string[0];
                    string stringValue = value.ToString();

                    if (!string.IsNullOrEmpty(stringValue))
                    {
                        //remove any white space following the seperator.
                        value = System.Text.RegularExpressions.Regex.Replace(value, @";\s+", ";");

                        //split the exlusion field string into an array
                        strArray = value.Split(SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
                    }

                    return (T)((object)strArray);
                }
            }
            else
                return DefaultValue;

        }

        public void Update(string key, string value)
        {
            var qry = new QueryExpression
            {
                EntityName = config.EntityName,
                ColumnSet = new ColumnSet(true),
                Criteria = new FilterExpression
                {
                    FilterOperator = LogicalOperator.And,
                    Conditions =
                    {
                        new ConditionExpression
                        {
                            AttributeName = config.NameColumn,
                            Operator = ConditionOperator.Equal,
                            Values = { key}
                        }
                    }
                }
            };

            var results = orgService.RetrieveMultiple(qry);

            if (results.Entities.Count == 0)
            {
                var record = new Entity(config.EntityName);
                record[config.ValueColumn] = value;
                record[config.NameColumn] = key;
                this.orgService.Create(record);
            }
            else
            {
                var record = new Entity(config.EntityName, results.Entities[0].Id);
                record[config.ValueColumn] = value;
                orgService.Update(record);
            }

            this.ClearCache();
        }

        public void ClearCache()
        {
            cache.Remove(CACHE_ENTRIES_KEY);
        }

    }
}
