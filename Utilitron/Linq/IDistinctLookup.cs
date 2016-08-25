using System.Collections.Generic;

namespace Utilitron.Linq
{
    /// <summary>
    ///     An <see cref="IMutableLookup{TKey,TValue}" /> with set semantics.
    ///     Every group is a set that can only contain distinct values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IDistinctLookup<TKey, TValue> : IMutableLookup<TKey, TValue>, IEnumerable<IDistinctGrouping<TKey, TValue>>
    {
        /// <summary>
        ///     Determines if the value exists in the IDistinctLookup for the given key.
        /// </summary>
        /// <param name="key">The key which will be searched.</param>
        /// <param name="value">The value with will be searched for.</param>
        /// <returns>True if the value exists for the given key, false otherwise.</returns>
        bool Contains(TKey key, TValue value);
    }
}