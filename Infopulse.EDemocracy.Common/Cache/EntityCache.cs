using Infopulse.EDemocracy.Common.Cache.Interfaces;
using System;

namespace Infopulse.EDemocracy.Common.Cache
{
	public class EntityCache : IEntityCache
	{
		private readonly ICacheProvider cacheProvider;

		public EntityCache(ICacheProvider cacheProvider)
		{
			this.cacheProvider = cacheProvider;
		}

		public void Add<T>(CachedElement key, T objectToCache)
		{
			this.cacheProvider.Add(key.ToString(), objectToCache);
		}

		public object Get(CachedElement key)
		{
			return this.cacheProvider.Get(key.ToString());
		}

		public object Get(CachedElement key, Func<object> getObject)
		{
			return this.cacheProvider.Get(key.ToString(), getObject);
		}

		public void Clear(CachedElement key)
		{
			this.cacheProvider.Clear(key.ToString());
		}
	}
}