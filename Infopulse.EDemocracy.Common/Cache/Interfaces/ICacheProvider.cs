using System;

namespace Infopulse.EDemocracy.Common.Cache.Interfaces
{
	public interface ICacheProvider
	{
		/// <summary>
		/// Adds object to cache. If already exists updates object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="key">Cached object key.</param>
		/// <param name="objectToCache">Object to cache.</param>
		void Add<T>(string key, T objectToCache);
		
		/// <summary>
		/// Gets object from Cache.
		/// </summary>
		/// <param name="key">Cached object key.</param>
		/// <returns>Object or null if object is not cached.</returns>
		object Get(string key);

		/// <summary>
		/// Gets object from Cache.
		/// </summary>
		/// <param name="key">Cached object key.</param>
		/// <param name="getObject">Function to get original object.</param>
		/// <returns>Cached object. If object is not cached it will be cached and returned.</returns>
		object Get(string key, Func<object> getObject);

		/// <summary>
		/// Clears cache by specific key.
		/// </summary>
		/// <param name="key">Cached object key.</param>
		void Clear(string key);
	}
}