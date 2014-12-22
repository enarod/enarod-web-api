using System;

namespace Infopulse.EDemocracy.Common.Cache.Interfaces
{
	public interface IEntityCache
	{
		/// <summary>
		/// Adds object to CacheProvider. If already exists updates object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">Cached element type.</param>
		/// <param name="objectToCache">Object to CacheProvider.</param>
		void Add<T>(CachedElement key, T objectToCache);

		/// <summary>
		/// Gets object from CacheProvider.
		/// </summary>
		/// <param name="key">Cached element type.</param>
		/// <returns>Object or null if object is not cached.</returns>
		object Get(CachedElement key);

		/// <summary>
		/// Gets object from CacheProvider.
		/// </summary>
		/// <param name="key">Cached element type.</param>
		/// <param name="getObject">Function to get original object.</param>
		/// <returns>Cached object. If object is not cached it will be cached and returned.</returns>
		object Get(CachedElement key, Func<object> getObject);

		/// <summary>
		/// Clears CacheProvider by specific key.
		/// </summary>
		/// <param name="key">Key to clear CacheProvider.</param>
		void Clear(CachedElement key);
	}
}