using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utilitron.Linq;

namespace Utilitron.Collections.Generic
{
    /// <summary>
    ///     Generic enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Adds a single item to the end of an enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the item to add.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="item">The item to add to the enumerable.</param>
        /// <returns>A new enumerable that includes the source followed by the added item.</returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, T item)
        {
            return source.Concat(item.InEnumerable());
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

        /// <summary>
        ///     Produces a count of items in the source that match a given condition along with the total number of items checked.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the source.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}" /> of items to count.</param>
        /// <param name="predicate">The predicate used to determine which items to count.</param>
        /// <returns>A <see cref="CountWithTotal" /> containing the total matching items and the total number of items checked.</returns>
        public static CountWithTotal CountWithTotal<TItem>(this IEnumerable<TItem> source, Predicate<TItem> predicate)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }

            if (predicate == null) { throw new ArgumentNullException(nameof(predicate)); }

            var count = 0;
            var total = 0;

            foreach (var item in source)
            {
                ++total;
                if (!predicate(item)) continue;
                ++count;
            }

            return new CountWithTotal(count, total);
        }

        /// <summary>
        ///     Gets the minimum and maximum value of a property from an enumerable at the same time.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the enumerable.</typeparam>
        /// <typeparam name="TMinMax">The type of the property to compare.</typeparam>
        /// <param name="source">The enumerable of input items.</param>
        /// <param name="selector">The function that defines the property to compare.</param>
        /// <returns>A <see cref="MinMax{T}" />With the minimum and maximum values of the property for the items.</returns>
        public static MinMax<TMinMax> MinMax<TItem, TMinMax>(this IEnumerable<TItem> source, Func<TItem, TMinMax> selector)
        {
            return MinMax(source, selector, Comparer<TMinMax>.Default);
        }

        /// <summary>
        ///     Gets the minimum and maximum value of a property from an enumerable at the same time.
        /// </summary>
        /// <typeparam name="TItem">The type of the items in the enumerable.</typeparam>
        /// <typeparam name="TMinMax">The type of the property to compare.</typeparam>
        /// <param name="source">The enumerable of input items.</param>
        /// <param name="selector">The function that defines the property to compare.</param>
        /// <param name="comparer">The comparer to use to determine the minimum and maximum values.</param>
        /// <returns>A <see cref="MinMax{T}" />With the minimum and maximum values of the property for the items.</returns>
        public static MinMax<TMinMax> MinMax<TItem, TMinMax>(this IEnumerable<TItem> source, Func<TItem, TMinMax> selector, IComparer<TMinMax> comparer)
        {
            if (source == null) { throw new ArgumentNullException(nameof(source)); }

            if (selector == null) { throw new ArgumentNullException(nameof(selector)); }

            if (comparer == null) { throw new ArgumentNullException(nameof(comparer)); }

            var min = default(TMinMax);
            var max = default(TMinMax);

            var first = true;
            foreach (var item in source)
            {
                var val = selector(item);

                if (first)
                {
                    min = val;
                    max = val;
                    first = false;
                }
                else
                {
                    if (comparer.Compare(min, val) > 0) { min = val; }

                    if (comparer.Compare(max, val) < 0) { max = val; }
                }
            }

            // If we never got past the first element then there is nothing in the source
            if (first) { throw new InvalidOperationException("Sequence contains no elements."); }

            return new MinMax<TMinMax>(min, max);
        }

        /// <summary>
        ///     Partition an enumerable into the given number of segments.
        ///     Order of input items is not preserved across segments.
        /// </summary>
        /// <typeparam name="T">The type of the items in the </typeparam>
        /// <param name="source">The initial item source.</param>
        /// <param name="partitions">The requested number of partitions.</param>
        /// <returns>The input items partitioned into the given number of segments.</returns>
        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> source, int partitions)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (partitions < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(partitions));
            }

            var output = new List<IEnumerable<T>>(partitions);

            // Initialize the internal lists to a reasonable default
            var total = (source as ICollection)?.Count ?? partitions;
            var length = total/partitions + (total%partitions != 0 ? 1 : 0);
            for (var i = 0; i < partitions; ++i)
            {
                output.Add(new List<T>(length));
            }

            // Go through every item in the source
            using (var enumerable = source.GetEnumerator())
            {
                for (var i = 0; enumerable.MoveNext(); ++i)
                {
                    ((IList<T>) output[i%partitions]).Add(enumerable.Current);
                }
            }

            return output;
        }
    }
}