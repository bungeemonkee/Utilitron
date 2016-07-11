using System.Collections.Generic;
using System.Linq;

namespace Utilitron
{
    /// <summary>
    /// Extensions for any object.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates an enumerable that only includes the given item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item to include in the enumerable.</param>
        /// <returns>An enumerable that contains only the given item.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this T item)
        {
            return Enumerable.Repeat(item, 1);
        }
    }
}
