using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using Infopulse.EDemocracy.Common.Cache.Interfaces;
using Infopulse.EDemocracy.Model;

namespace Infopulse.EDemocracy.Common.Cache
{
	public class Cache : ICache
	{
		private MemoryCache cache = MemoryCache.Default;

		public readonly Dictionary<CachedElement, Type> CachingMap =
			new Dictionary<CachedElement, Type>()
			{
				{CachedElement.PetitionLevel, typeof(IEnumerable<PetitionLevel>)},
				{CachedElement.PetitionCategory, typeof(IEnumerable<Entity>)}
			};

		public void Add<T>(CachedElement key, T objectToCache)
		{
			var cachedObject = this.cache.Get(key.ToString());
			if (cachedObject != null)
			{
				this.cache.Remove(key.ToString());
			}

			AddToCache(key, objectToCache);
		}

		public object Get(CachedElement key)
		{
			var cachedObject = this.cache.Get(key.ToString());
			return cachedObject;
		}

		public object Get(CachedElement key, Func<object> getObject)
		{
			var cachedObject = this.cache.Get(key.ToString());
			if (cachedObject == null)
			{
				var objectToCache = getObject();
				AddToCache(key, objectToCache);
				cachedObject = objectToCache;
			}

			return cachedObject;
		}

		public void Clear(CachedElement key)
		{
			this.cache.Remove(key.ToString());
		}

		private void AddToCache(CachedElement key, object objectToCache)
		{
			this.cache.Add(key.ToString(), objectToCache, DateTimeOffset.Now.AddHours(1));
		}
	}
}