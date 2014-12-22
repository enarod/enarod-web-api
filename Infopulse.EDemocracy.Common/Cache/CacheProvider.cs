using System;
using System.Runtime.Caching;
using Infopulse.EDemocracy.Common.Cache.Interfaces;

namespace Infopulse.EDemocracy.Common.Cache
{
	public class CacheProvider : ICacheProvider
	{
		private MemoryCache cache = MemoryCache.Default;

		public void Add<T>(string key, T objectToCache)
		{
			var cachedObject = this.cache.Get(key);
			if (cachedObject != null)
			{
				this.cache.Remove(key.ToString());
			}

			AddToCache(key, objectToCache);
		}

		public object Get(string key)
		{
			var cachedObject = this.cache.Get(key.ToString());
			return cachedObject;
		}

		public object Get(string key, Func<object> getObject)
		{
			var cachedObject = this.cache.Get(key);
			if (cachedObject == null)
			{
				var objectToCache = getObject();
				AddToCache(key, objectToCache);
				cachedObject = objectToCache;
			}

			return cachedObject;
		}

		public void Clear(string key)
		{
			this.cache.Remove(key);
		}

		private void AddToCache(string key, object objectToCache)
		{
			this.cache.Add(key, objectToCache, DateTimeOffset.Now.AddHours(1));
		}
	}
}