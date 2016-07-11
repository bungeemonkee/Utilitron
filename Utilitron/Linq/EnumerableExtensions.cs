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
    }
}
