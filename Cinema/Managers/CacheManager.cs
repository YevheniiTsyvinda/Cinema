using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Cinema.Managers
{
    public class CacheManager : ICacheManager
    {
        private const string DependencyKey = "Cinema.CacheManager.Dependency";

        public CacheManager()
        {
            HttpRuntime.Cache.Add(DependencyKey,
                DateTime.Now,
                null,
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default,
                null);
        }
        public void Clear()
        {
            Cache c = HttpRuntime.Cache;
            c.Insert(
                DependencyKey,
                DateTime.Now,
                null,
                Cache.NoAbsoluteExpiration,
                Cache.NoSlidingExpiration,
                CacheItemPriority.Default,
                null);
        }

        public T Get<T>(string key) where T : class
        {
            try
            {
                return HttpRuntime.Cache[key] as T;
            }
            catch 
            {
                return null;
            }
        }

        public void Insert<T>(T item, string key)
        {
            if(item == null)
            {
                return;
            }
            Insert(item, key, Constants.DefaultCacheTime);
        }

        public void Insert<T>(T item, string key, DateTime expidationTime)
        {
            if (item ==null)
            {
                return;
            }
            var casheDependency = new CacheDependency(null, new[] { DependencyKey });
            HttpRuntime.Cache.Insert(key, item, casheDependency, expidationTime, Cache.NoSlidingExpiration);
        }

        public void Insert<T>(T item, string key, TimeSpan expirationSpan)
        {
            if (item == null)
            {
                return;
            }
            var casheDependency = new CacheDependency(null, new[] { DependencyKey });
            HttpRuntime.Cache.Insert(key, item, casheDependency, Cache.NoAbsoluteExpiration , expirationSpan);
        }
    }
}