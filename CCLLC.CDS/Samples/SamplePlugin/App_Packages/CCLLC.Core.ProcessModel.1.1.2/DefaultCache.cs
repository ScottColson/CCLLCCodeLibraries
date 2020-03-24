using System;
using System.Runtime.Caching;

namespace CCLLC.Core
{
    /// <summary>
    /// Implements cache based on default <see cref="MemoryCache"/>.
    /// </summary>
    public class DefaultCache : ICache
    {    
        private static MemoryCache Cache
        {
            get { return MemoryCache.Default; }
        }

        public DefaultCache() { }      

        public void Add(string key, object data, int seconds)
        {
            if (seconds < 0) { seconds = 0; }
            this.Add(key, data, TimeSpan.FromSeconds(seconds));
        }

        public void Add(string key, object data, TimeSpan lifetime)
        {
            if (lifetime == default(TimeSpan)) { lifetime = new TimeSpan(0, 5, 0); }
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + lifetime };
            Cache.Add(key, data, policy);
        }

        public void Add<T>(string key, T data, int seconds)
        {
            if (seconds < 0) { seconds = 0; }
            this.Add<T>(key, data, TimeSpan.FromSeconds(seconds));
        }

        public void Add<T>(string key, T data, TimeSpan lifetime)
        {
            if (lifetime == default(TimeSpan)) { lifetime = new TimeSpan(0, 5, 0); }
            CacheItemPolicy policy = new CacheItemPolicy { AbsoluteExpiration = DateTime.Now + lifetime };
            Cache.Add(key, data, policy);
        }

        public object Get(string key)
        {           
            return Cache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)Get(key);
        }

        public void Remove(string key)
        {
            if (Exists(key))
            {
                Cache.Remove(key);
            }
        }

        public bool Exists(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return Cache[key] != null;
            }

            return false;
        }

    }

}
