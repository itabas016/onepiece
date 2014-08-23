using OnePiece.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace OnePiece.Framework.Cache
{
    /// <summary>
    /// Currently, Asp.Net.MemoryCache is used as the cache intermediary; 
    /// You know, the disadvantage is it could not be used between mutiple processes, like web garden.
    /// If so, we need rewrite the logic, maybe redis is a candidate.
    /// </summary>
    public class MemoryCacheManager : SingletonBase<MemoryCacheManager>, ICacheManager
    {
        ObjectCache _cache = MemoryCache.Default;

        public void Add(string key, object value)
        {
            Add(key, value, 600);
        }

        public void Add(string key, object value, int timeoutSeconds)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(timeoutSeconds);

            _cache.Set(key, value, policy);
        }

        public void Add(string key, object value, DateTime? absoluteExpiration)
        {
            if (absoluteExpiration == null) absoluteExpiration = DateTime.Now.AddHours(1);

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = new DateTimeOffset(absoluteExpiration.GetValueOrDefault());

            _cache.Set(key, value, policy);
        }

        public void Add(string key, object value, TimeSpan? slidingExpiration)
        {
            Add(key, value, DateTime.Now.Add(slidingExpiration.GetValueOrDefault()));
        }

        public bool Contains(string key)
        {
            return _cache.Contains(key);
        }

        public object Get(string key, Type dataType)
        {
            return _cache.Get(key);
        }

        public T Get<T>(string key)
        {
            return (T)_cache.Get(key);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void Clear(string prefix = "SVC")
        {
            var cacheKeys = _cache.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                if (!prefix.IsNullOrEmpty() && cacheKey.StartsWith(prefix))
                    _cache.Remove(cacheKey);
            }
        }

        public void Flush()
        {
            var cacheKeys = _cache.Select(kvp => kvp.Key).ToList();
            foreach (string cacheKey in cacheKeys)
            {
                _cache.Remove(cacheKey);
            }
        }
    }
}
