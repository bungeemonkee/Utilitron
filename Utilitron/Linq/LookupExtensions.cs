using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     Generic extensions for <see cref="ILookup{TKey, TElement}" /> objects.
    /// </summary>
    public static class LookupExtensions
    {
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
    }
}
