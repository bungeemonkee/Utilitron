using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     Extensions for any <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Adds a single item to an enumerable.
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
        ///     Create a lookup with a 2-item tuple as keys.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key item.</typeparam>
        /// <typeparam name="TKey2">The type of the second key item.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <param name="elements">The elements used to make the lookup.</param>
        /// <param name="key1Selector">Function to extract the first key item from each element.</param>
        /// <param name="key2Selector">Function to extract the second key item from each element.</param>
        /// <returns>A lookup indexed by a two-item tuple.</returns>
        public static ILookup<Tuple<TKey1, TKey2>, TValue> ToLookup2<TKey1, TKey2, TValue>(this IEnumerable<TValue> elements, Func<TValue, TKey1> key1Selector, Func<TValue, TKey2> key2Selector)
        {
            return elements.ToLookup(x => new Tuple<TKey1, TKey2>(key1Selector(x), key2Selector(x)));
        }

        /// <summary>
        ///     Create a lookup with a 2-item tuple as keys.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key item.</typeparam>
        /// <typeparam name="TKey2">The type of the second key item.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of the elements used to make the lookup.</typeparam>
        /// <param name="elements">The elements used to make the lookup.</param>
        /// <param name="key1Selector">Function to extract the first key item from each element.</param>
        /// <param name="key2Selector">Function to extract the second key item from each element.</param>
        /// <param name="valueSelector">Function to extract the value from each element.</param>
        /// <returns>A lookup indexed by a two-item tuple.</returns>
        public static ILookup<Tuple<TKey1, TKey2>, TValue> ToLookup2<TKey1, TKey2, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey1> key1Selector, Func<TElement, TKey2> key2Selector, Func<TElement, TValue> valueSelector)
        {
            return elements.ToLookup(x => new Tuple<TKey1, TKey2>(key1Selector(x), key2Selector(x)), valueSelector);
        }

        /// <summary>
        ///     Create a lookup with a 2-item tuple as keys.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key item.</typeparam>
        /// <typeparam name="TKey2">The type of the second key item.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of the elements used to make the lookup.</typeparam>
        /// <param name="elements">The elements used to make the lookup.</param>
        /// <param name="key1Selector">Function to extract the first key item from each element.</param>
        /// <param name="key2Selector">Function to extract the second key item from each element.</param>
        /// <param name="valueSelector">Function to extract the value from each element.</param>
        /// <param name="keyComparer">Comparer to use when determining key equality.</param>
        /// <returns>A lookup indexed by a two-item tuple.</returns>
        public static ILookup<Tuple<TKey1, TKey2>, TValue> ToLookup2<TKey1, TKey2, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey1> key1Selector, Func<TElement, TKey2> key2Selector, Func<TElement, TValue> valueSelector, IEqualityComparer<Tuple<TKey1, TKey2>> keyComparer)
        {
            return elements.ToLookup(x => new Tuple<TKey1, TKey2>(key1Selector(x), key2Selector(x)), valueSelector, keyComparer);
        }

        /// <summary>
        ///     Create a lookup with a 3-item tuple as keys.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key item.</typeparam>
        /// <typeparam name="TKey2">The type of the second key item.</typeparam>
        /// <typeparam name="TKey3">The type of the third key item.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <param name="elements">The elements used to make the lookup.</param>
        /// <param name="key1Selector">Function to extract the first key item from each element.</param>
        /// <param name="key2Selector">Function to extract the second key item from each element.</param>
        /// <param name="key3Selector">Function to extract the third key item from each element.</param>
        /// <returns>A lookup indexed by a three-item tuple.</returns>
        public static ILookup<Tuple<TKey1, TKey2, TKey3>, TValue> ToLookup3<TKey1, TKey2, TKey3, TValue>(this IEnumerable<TValue> elements, Func<TValue, TKey1> key1Selector, Func<TValue, TKey2> key2Selector, Func<TValue, TKey3> key3Selector)
        {
            return elements.ToLookup(x => new Tuple<TKey1, TKey2, TKey3>(key1Selector(x), key2Selector(x), key3Selector(x)));
        }

        /// <summary>
        ///     Create a lookup with a 3-item tuple as keys.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key item.</typeparam>
        /// <typeparam name="TKey2">The type of the second key item.</typeparam>
        /// <typeparam name="TKey3">The type of the third key item.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of the elements used to make the lookup.</typeparam>
        /// <param name="elements">The elements used to make the lookup.</param>
        /// <param name="key1Selector">Function to extract the first key item from each element.</param>
        /// <param name="key2Selector">Function to extract the second key item from each element.</param>
        /// <param name="key3Selector">Function to extract the third key item from each element.</param>
        /// <param name="valueSelector">Function to extract the value from each element.</param>
        /// <returns>A lookup indexed by a three-item tuple.</returns>
        public static ILookup<Tuple<TKey1, TKey2, TKey3>, TValue> ToLookup3<TKey1, TKey2, TKey3, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey1> key1Selector, Func<TElement, TKey2> key2Selector, Func<TElement, TKey3> key3Selector, Func<TElement, TValue> valueSelector)
        {
            return elements.ToLookup(x => new Tuple<TKey1, TKey2, TKey3>(key1Selector(x), key2Selector(x), key3Selector(x)), valueSelector);
        }

        /// <summary>
        ///     Create a lookup with a 3-item tuple as keys.
        /// </summary>
        /// <typeparam name="TKey1">The type of the first key item.</typeparam>
        /// <typeparam name="TKey2">The type of the second key item.</typeparam>
        /// <typeparam name="TKey3">The type of the third key item.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of the elements used to make the lookup.</typeparam>
        /// <param name="elements">The elements used to make the lookup.</param>
        /// <param name="key1Selector">Function to extract the first key item from each element.</param>
        /// <param name="key2Selector">Function to extract the second key item from each element.</param>
        /// <param name="key3Selector">Function to extract the third key item from each element.</param>
        /// <param name="valueSelector">Function to extract the value from each element.</param>
        /// <param name="keyComparer">Comparer to use when determining key equality.</param>
        /// <returns>A lookup indexed by a three-item tuple.</returns>
        public static ILookup<Tuple<TKey1, TKey2, TKey3>, TValue> ToLookup3<TKey1, TKey2, TKey3, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey1> key1Selector, Func<TElement, TKey2> key2Selector, Func<TElement, TKey3> key3Selector, Func<TElement, TValue> valueSelector, IEqualityComparer<Tuple<TKey1, TKey2, TKey3>> keyComparer)
        {
            return elements.ToLookup(x => new Tuple<TKey1, TKey2, TKey3>(key1Selector(x), key2Selector(x), key3Selector(x)), valueSelector, keyComparer);
        }

        /// <summary>
        ///     Create an <see cref="ILookup{TKey,TElement}" /> with distinct element sets.
        /// </summary>
        /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <param name="elements">The input elements.</param>
        /// <param name="keySelector">A function to get the keys from the input elements.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}" /> with distinct elements.</returns>
        public static ILookup<TKey, TValue> ToLookupDistinct<TKey, TValue>(this IEnumerable<TValue> elements, Func<TValue, TKey> keySelector)
        {
            return ToLookupDistinct(elements, keySelector, x => x, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
        }

        /// <summary>
        ///     Create an <see cref="ILookup{TKey,TElement}" /> with distinct element sets.
        /// </summary>
        /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of  the input elements.</typeparam>
        /// <param name="elements">The input elements.</param>
        /// <param name="keySelector">A function to get the keys from the input elements.</param>
        /// <param name="valueSelector">A function to get the values from the input eleemnts.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}" /> with distinct elements.</returns>
        public static ILookup<TKey, TValue> ToLookupDistinct<TKey, TValue, TElement>(this IEnumerable<TElement> elements, Func<TElement, TKey> keySelector, Func<TElement, TValue> valueSelector)
        {
            return ToLookupDistinct(elements, keySelector, valueSelector, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
        }

        /// <summary>
        ///     Create an <see cref="ILookup{TKey,TElement}" /> with distinct element sets.
        /// </summary>
        /// <typeparam name="TKey">The type of the lookup keys.</typeparam>
        /// <typeparam name="TValue">The type of the lookup values.</typeparam>
        /// <typeparam name="TElement">The type of  the input elements.</typeparam>
        /// <param name="elements">The input elements.</param>
        /// <param name="keySelector">A function to get the keys from the input elements.</param>
        /// <param name="valueSelector">A function to get the values from the input eleemnts.</param>
        /// <param name="keyComparer">The comparer used for the keys.</param>
        /// <param name="valueComparer">The comparer used for the values.</param>
        /// <returns>An <see cref="ILookup{TKey,TElement}" /> with distinct elements.</returns>
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