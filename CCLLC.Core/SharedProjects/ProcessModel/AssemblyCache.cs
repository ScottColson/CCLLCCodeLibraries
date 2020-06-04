using System;
using System.Collections.Generic;
using System.Linq;

namespace CCLLC.Core
{
    public class AssemblyCache : ICache
    {
        
        class CacheItem
        {
            private readonly DateTime expirationDate;

            public CacheItem(string key, object value, TimeSpan lifetime)
            {
                this.Key = key;
                this.Value = value;
                this.expirationDate = DateTime.Now + lifetime;
            }

            public string Key { get; }            
            public object Value { get; }

            public bool IsCurrent => DateTime.Now < expirationDate;

            public override int GetHashCode()
            {
                return StringComparer.InvariantCultureIgnoreCase
                             .GetHashCode(this.Key);
            }

            public override bool Equals(object obj)
            {
                return this.Key == ((CacheItem)obj).Key;                
            }


        }

        private static HashSet<CacheItem> cacheItems;

        public AssemblyCache()
        {
            lock (this)
            {
                if(cacheItems is null)
                {
                    cacheItems = new HashSet<CacheItem>();
                }
            }
        }

        public void Add(string key, object data, TimeSpan lifetime)
        {
            if (lifetime == default(TimeSpan)) { lifetime = new TimeSpan(0, 5, 0); }
            lock (cacheItems)
            {                
                cacheItems.Add(new CacheItem(key, data, lifetime));
            }
        }

        public void Add(string key, object data, int seconds)
        {
            if (seconds < 0) { seconds = 0; }
            this.Add(key, data, TimeSpan.FromSeconds(seconds));
        }       

        public void Add<T>(string key, T data, int seconds)
        {
            if (seconds < 0) { seconds = 0; }
            this.Add(key, data, TimeSpan.FromSeconds(seconds));
        }

        public void Add<T>(string key, T data, TimeSpan lifetime)
        {
            if (lifetime == default(TimeSpan)) { lifetime = new TimeSpan(0, 5, 0); }
            lock (cacheItems)
            {
                cacheItems.Add(new CacheItem(key, data, lifetime));
            }
        }

        public object Get(string key)
        {
            var item = cacheItems.Where(i => i.Key == key && i.IsCurrent).Select(i => i.Value).FirstOrDefault();
            return item;
        }

        public T Get<T>(string key)
        {
            return (T)Get(key);
        }

        public void Remove(string key)
        {
            var item = GetCacheItem(key);
            if (item != null)
            {
                cacheItems.Remove(item);
            }
        }

        public bool Exists(string key)
        {
            return GetCacheItem(key)?.IsCurrent == true;           
        }

        private CacheItem GetCacheItem(string key)
        {
            return cacheItems.Where(i => i.Key == key).FirstOrDefault();
        }
    }
}
