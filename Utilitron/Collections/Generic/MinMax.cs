
namespace Utilitron.Collections.Generic
{
    /// <summary>
    ///     Represents a minimum and maximum value of a type.
    /// </summary>
    /// <typeparam name="T">The type of the min/max values.</typeparam>
    public struct MinMax<T>
    {
        /// <summary>
        ///     The minimum value.
        /// </summary>
        public readonly T Min;

        /// <summary>
        ///     The maximum value.
        /// </summary>
        public readonly T Max;

        /// <summary>
        ///     Create a new instance with the given minimum and maximum values.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        public MinMax(T min, T max)
            : this()
        {
            Min = min;
            Max = max;
        }
    }
}
