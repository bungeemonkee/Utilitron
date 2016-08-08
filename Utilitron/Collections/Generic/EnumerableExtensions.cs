using System;
using System.Collections;
using System.Collections.Generic;

namespace Utilitron.Collections.Generic
{
    /// <summary>
    /// Generic enumerable extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Partition an enumerable into the given number of segments.
        /// Order of input items is not preserved across segments.
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
            var length = (total / partitions) + (total % partitions != 0 ? 1 : 0);
            for (var i = 0; i < partitions; ++i)
            {
                output.Add(new List<T>(length));
            }

            // Go through every item in the source
            using (var enumerable = source.GetEnumerator())
            {
                for (var i = 0; enumerable.MoveNext(); ++i)
                {
                    ((IList<T>)output[i % partitions]).Add(enumerable.Current);
                }
            }

            return output;
        }
    }
}
