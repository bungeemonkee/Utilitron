using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    /// Extensions for any <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Adds a single item to an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the item to add.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="item">The item to add to the enumerable.</param>
        /// <returns>A new enumerable that includes the source and the added item.</returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
        {
            return source.Concat(item.AsEnumerable());
        }

        /// <summary>
        /// Create an <see cref="ILookup{TKey,TElement}"/> with distinct element sets.
        /// </summary>
        /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <param name="elements">The input elements.</param>
        /// <param name="keySelector">A function to get the keys from the input elements.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}"/> with distinct elements.</returns>
        public static ILookup<TKey, TValue> ToLookupDistinct<TKey, TValue>(this IEnumerable<TValue> elements, Func<TValue, TKey> keySelector)
        {
            return ToLookupDistinct(elements, keySelector, x => x, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
        }

        /// <summary>
        /// Create an <see cref="ILookup{TKey,TElement}"/> with distinct element sets.
        /// </summary>
        /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of  the input elements.</typeparam>
        /// <param name="elements">The input elements.</param>
        /// <param name="keySelector">A function to get the keys from the input elements.</param>
        /// <param name="valueSelector">A function to get the values from the input eleemnts.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}"/> with distinct elements.</returns>
        public static ILookup<TKey, TValue> ToLookupDistinct<TKey, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey> keySelector, Func<TElement, TValue> valueSelector)
        {
            return ToLookupDistinct(elements, keySelector, valueSelector, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
        }

        /// <summary>
        /// Create an <see cref="ILookup{TKey,TElement}"/> with distinct element sets.
        /// </summary>
        /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of  the input elements.</typeparam>
        /// <param name="elements">The input elements.</param>
        /// <param name="keySelector">A function to get the keys from the input elements.</param>
        /// <param name="valueSelector">A function to get the values from the input eleemnts.</param>
        /// <param name="keyComparer">The comparer used for the keys.</param>
        /// <param name="valueComparer">The comparer used for the values.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}"/> with distinct elements.</returns>
        public static ILookup<TKey, TValue> ToLookupDistinct<TKey, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey> keySelector, Func<TElement, TValue> valueSelector, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            var result = new DistinctLookup<TKey, TValue>(keyComparer, valueComparer);

            foreach (var item in elements)
            {
                var key = keySelector(item);
                var value = valueSelector(item);

                if (result.Contains(key, value)) continue;

                result.Add(key, value);
            }

            return result;
        }
    }
}
