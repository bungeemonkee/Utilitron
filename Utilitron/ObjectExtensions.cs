using System.Collections.Generic;
using System.Linq;

namespace Utilitron
{
    /// <summary>
    ///     Extensions for any object.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     Creates an enumerable that only includes the given item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item to include in the enumerable.</param>
        /// <returns>An enumerable that contains only the given item.</returns>
        public static IEnumerable<T> InEnumerable<T>(this T item)
        {
            return Enumerable.Repeat(item, 1);
        }

        /// <summary>
        ///     Creates an enumerable that starts with this object and continues with the given enumerable.
        ///     Effectively adds this object to the beginning of the enumerable.
        /// </summary>
        /// <typeparam name="T">The type of the item to add.</typeparam>
        /// <param name="item">The item to add to the enumerable.</param>
        /// <param name="items">The source enumerable.</param>
        /// <returns>A new enumerable that includes the source after this item.</returns>
        public static IEnumerable<T> Concat<T>(this T item, IEnumerable<T> items)
        {
            return item.InEnumerable().Concat(items);
        }
    }
}