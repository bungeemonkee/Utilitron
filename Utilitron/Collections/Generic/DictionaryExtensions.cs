using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Collections.Generic
{
    /// <summary>
    ///     Generic extensions for <see cref="IDictionary{TKey,TValue}" /> objects.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Gets a value from a dictionary or the default value if the given key does not exist in the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The dictionary key type.</typeparam>
        /// <typeparam name="TValue">The dictionary value type.</typeparam>
        /// <param name="dictionary">The dictionary to search.</param>
        /// <param name="key">The key to search the dictionary for.</param>
        /// <returns>The value for the given key or the default value for that type if the key is not found.</returns>
        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            TValue value;
            return dictionary.TryGetValue(key, out value)
                ? value
                : default(TValue);
        }

        /// <summary>
        ///     Converts an <see cref="IDictionary{TKey,TValue}" /> to an <see cref="ILookup{TKey,TElement}" /> by swapping the
        ///     keys and values.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary">The dictionary to convert.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}" /> created by using the dictionary values as lookup keys and the dictionary keys as values.</returns>
        public static ILookup<TValue, TKey> ToLookup<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.ToLookup(x => x.Value, x => x.Key);
        }
    }
}