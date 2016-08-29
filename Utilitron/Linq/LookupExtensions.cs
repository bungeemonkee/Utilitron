using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     Generic extensions for <see cref="ILookup{TKey,TElement}" /> objects.
    /// </summary>
    public static class LookupExtensions
    {
        /// <summary>
        ///     Determines if a 3-item tupple exists in a lookup as its key.
        /// </summary>
        /// <typeparam name="TKey1">The lookup first key type.</typeparam>
        /// <typeparam name="TKey2">The lookup second key type.</typeparam>
        /// <typeparam name="TValue">The lookup value type.</typeparam>
        /// <param name="lookup">The lookup to search.</param>
        /// <param name="key1">The first part of the key to search the lookup for.</param>
        /// <param name="key2">The second part of the key to search the lookup for.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        public static bool Contains<TKey1, TKey2, TValue>(this ILookup<Tuple<TKey1, TKey2>, TValue> lookup, TKey1 key1, TKey2 key2)
        {
            var key = new Tuple<TKey1, TKey2>(key1, key2);
            return lookup.Contains(key);
        }

        /// <summary>
        ///     Determines if a 3-item tupple exists in a lookup as its key.
        /// </summary>
        /// <typeparam name="TKey1">The lookup first key type.</typeparam>
        /// <typeparam name="TKey2">The lookup second key type.</typeparam>
        /// <typeparam name="TKey3">The lookup third key type.</typeparam>
        /// <typeparam name="TValue">The lookup value type.</typeparam>
        /// <param name="lookup">The lookup to search.</param>
        /// <param name="key1">The first part of the key to search the lookup for.</param>
        /// <param name="key2">The second part of the key to search the lookup for.</param>
        /// <param name="key3">The third part of the key to search the lookup for.</param>
        /// <returns>True if the key exists, false otherwise.</returns>
        public static bool Contains<TKey1, TKey2, TKey3, TValue>(this ILookup<Tuple<TKey1, TKey2, TKey3>, TValue> lookup, TKey1 key1, TKey2 key2, TKey3 key3)
        {
            var key = new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3);
            return lookup.Contains(key);
        }

        /// <summary>
        ///     Gets a value from a lookup or the default value if the given key does not exist in the lookup.
        /// </summary>
        /// <typeparam name="TKey">The lookup key type.</typeparam>
        /// <typeparam name="TValue">The lookup value type.</typeparam>
        /// <param name="lookup">The lookup to search.</param>
        /// <param name="key">The key to search the lookup for.</param>
        /// <returns>The value for the given key or the default value for that type if the key is not found.</returns>
        public static IEnumerable<TValue> GetOrDefault<TKey, TValue>(this ILookup<TKey, TValue> lookup, TKey key)
        {
            return lookup.Contains(key)
                ? lookup[key]
                : null;
        }

        /// <summary>
        ///     Gets a value from a lookup or the default value if the given key does not exist in the lookup.
        /// </summary>
        /// <typeparam name="TKey1">The lookup first key type.</typeparam>
        /// <typeparam name="TKey2">The lookup second key type.</typeparam>
        /// <typeparam name="TValue">The lookup value type.</typeparam>
        /// <param name="lookup">The lookup to search.</param>
        /// <param name="key1">The first part of the key to search the lookup for.</param>
        /// <param name="key2">The second part of the key to search the lookup for.</param>
        /// <returns>The value for the given key or the default value for that type if the key is not found.</returns>
        public static IEnumerable<TValue> GetOrDefault<TKey1, TKey2, TValue>(this ILookup<Tuple<TKey1, TKey2>, TValue> lookup, TKey1 key1, TKey2 key2)
        {
            var key = new Tuple<TKey1, TKey2>(key1, key2);
            return lookup.Contains(key)
                ? lookup[key]
                : null;
        }

        /// <summary>
        ///     Gets a value from a lookup or the default value if the given key does not exist in the lookup.
        /// </summary>
        /// <typeparam name="TKey1">The lookup first key type.</typeparam>
        /// <typeparam name="TKey2">The lookup second key type.</typeparam>
        /// <typeparam name="TKey3">The lookup third key type.</typeparam>
        /// <typeparam name="TValue">The lookup value type.</typeparam>
        /// <param name="lookup">The lookup to search.</param>
        /// <param name="key1">The first part of the key to search the lookup for.</param>
        /// <param name="key2">The second part of the key to search the lookup for.</param>
        /// <param name="key3">The third part of the key to search the lookup for.</param>
        /// <returns>The value for the given key or the default value for that type if the key is not found.</returns>
        public static IEnumerable<TValue> GetOrDefault<TKey1, TKey2, TKey3, TValue>(this ILookup<Tuple<TKey1, TKey2, TKey3>, TValue> lookup, TKey1 key1, TKey2 key2, TKey3 key3)
        {
            var key = new Tuple<TKey1, TKey2, TKey3>(key1, key2, key3);
            return lookup.Contains(key)
                ? lookup[key]
                : null;
        }
    }
}