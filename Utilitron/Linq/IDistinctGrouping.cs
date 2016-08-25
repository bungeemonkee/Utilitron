using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     An <see cref="IGrouping{TKey,TElement}" /> with set semantics.
    ///     Every group is a set that can only contain distinct values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public interface IDistinctGrouping<out TKey, TValue> : IGrouping<TKey, TValue>
    {
        /// <summary>
        ///     Determines if the IDistinctGrouping contains the value.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if the value exist in the IDistinctGrouping, false otherwise.</returns>
        bool Contains(TValue value);
    }
}