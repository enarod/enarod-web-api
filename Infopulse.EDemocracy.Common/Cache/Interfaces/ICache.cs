using System;

namespace Infopulse.EDemocracy.Common.Cache.Interfaces
{
	public interface ICache
	{
		/// <summary>
		/// Adds object to cache. If already exists updates object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">Cached element type.</param>
		/// <param name="objectToCache">Object to cache.</param>
		void Add<T>(CachedElement key, T objectToCache);
		
		/// <summary>
		/// Gets object from Cache.
		/// </summary>
		/// <param name="key">Cached element type.</param>
		/// <returns>Object or null if object is not cached.</returns>
		object Get(CachedElement key);

		/// <summary>
		/// Gets object from Cache.
		/// </summary>
		/// <param name="key">Cached element type.</param>
		/// <param name="getObject">Function to get original object.</param>
		/// <returns>Cached object. If object is not cached it will be cached and returned.</returns>
		object Get(CachedElement key, Func<object> getObject);

		/// <summary>
		/// Clears cache by specific key.
		/// </summary>
		/// <param name="key">Key to clear cache.</param>
		void Clear(CachedElement key);
	}
}