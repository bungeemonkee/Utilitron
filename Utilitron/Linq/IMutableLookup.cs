using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     An <see cref="ILookup{TKey,TElement}" /> that allows adding new keys and values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IMutableLookup<TKey, TValue>: ILookup<TKey, TValue>
    {
        /// <summary>
        ///     Adds a new value to the IMutableLookup for the given key.
        /// </summary>
        /// <param name="key">The key to which the value will be added.</param>
        /// <param name="value">The value to add for the given key.</param>
        void Add(TKey key, TValue value);
    }
}
