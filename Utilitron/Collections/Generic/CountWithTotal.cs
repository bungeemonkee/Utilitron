
namespace Utilitron.Collections.Generic
{
    /// <summary>
    ///     A count of elements matching a condition that includes a count of the total elements checked against the condition.
    /// </summary>
    public struct CountWithTotal
    {
        /// <summary>
        ///     The number of elements that matches the condition.
        /// </summary>
        public readonly int Count;

        /// <summary>
        ///     The number of elements checked against the condition.
        /// </summary>
        public readonly int Total;

        /// <summary>
        ///     Create a new instance with the given count and total.
        /// </summary>
        /// <param name="count"></param>
        /// <param name="total"></param>
        public CountWithTotal(int count, int total)
            : this()
        {
            Count = count;
            Total = total;
        }

        /// <summary>
        ///     Did any of the items match?
        /// </summary>
        /// <returns>True if <see cref="Count" /> is greater than zero, false otherwise.</returns>
        public bool Any()
        {
            return Count > 0;
        }

        /// <summary>
        ///     Did all of the items match?
        /// </summary>
        /// <returns>True if <see cref="Count" /> is non-zero and equals <see cref="Total" />, false otherwise.</returns>
        public bool All()
        {
            return Count != 0 && Count == Total;
        }
    }
}
