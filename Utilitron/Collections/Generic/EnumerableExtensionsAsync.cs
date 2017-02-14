using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utilitron.Collections.Generic
{
    /// <summary>
    ///     Generic enumerable extensions for tasks that return an enumerable.
    /// </summary>
    /// <remarks>
    ///     These are only in a separate class because the first was getting big.
    /// </remarks>
    public static class EnumerableExtensionsAsync
    {
        /// <summary>
        /// Calls <see cref="Enumerable.Single"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> Single<T>(this Task<IEnumerable<T>> enumerableTask)
        {
            var enumerable = await enumerableTask;
            return enumerable.Single();
        }

        /// <summary>
        /// Calls <see cref="Enumerable.Single"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> Single<T>(this Task<IEnumerable<T>> enumerableTask, Func<T, bool> predicate)
        {
            var enumerable = await enumerableTask;
            return enumerable.Single(predicate);
        }

        /// <summary>
        /// Calls <see cref="Enumerable.SingleOrDefault"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> SingleOrDefault<T>(this Task<IEnumerable<T>> enumerableTask)
        {
            var enumerable = await enumerableTask;
            return enumerable.SingleOrDefault();
        }

        /// <summary>
        /// Calls <see cref="Enumerable.SingleOrDefault"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> SingleOrDefault<T>(this Task<IEnumerable<T>> enumerableTask, Func<T, bool> predicate)
        {
            var enumerable = await enumerableTask;
            return enumerable.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Calls <see cref="Enumerable.First"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> First<T>(this Task<IEnumerable<T>> enumerableTask)
        {
            var enumerable = await enumerableTask;
            return enumerable.First();
        }

        /// <summary>
        /// Calls <see cref="Enumerable.First"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> First<T>(this Task<IEnumerable<T>> enumerableTask, Func<T, bool> predicate)
        {
            var enumerable = await enumerableTask;
            return enumerable.First(predicate);
        }

        /// <summary>
        /// Calls <see cref="Enumerable.FirstOrDefault"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> FirstOrDefault<T>(this Task<IEnumerable<T>> enumerableTask)
        {
            var enumerable = await enumerableTask;
            return enumerable.FirstOrDefault();
        }

        /// <summary>
        /// Calls <see cref="Enumerable.FirstOrDefault"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T> FirstOrDefault<T>(this Task<IEnumerable<T>> enumerableTask, Func<T, bool> predicate)
        {
            var enumerable = await enumerableTask;
            return enumerable.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Calls <see cref="Enumerable.ToList"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<List<T>> ToList<T>(this Task<IEnumerable<T>> enumerableTask)
        {
            var enumerable = await enumerableTask;
            return enumerable.ToList();
        }

        /// <summary>
        /// Calls <see cref="Enumerable.ToArray"/> after await-ing for an enumerable.
        /// </summary>
        public static async Task<T[]> ToArray<T>(this Task<IEnumerable<T>> enumerableTask)
        {
            var enumerable = await enumerableTask;
            return enumerable.ToArray();
        }
    }
}