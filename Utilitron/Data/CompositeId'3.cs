namespace Utilitron.Data
{
    /// <summary>
    ///     An identifier type used to represent composite identifiers.
    ///     Primarily for use with <see cref="Model{TIdentifier,TModel}" /> objects.
    /// </summary>
    /// <typeparam name="TKey1">The type of the first id field.</typeparam>
    /// <typeparam name="TKey2">The type of the second id field.</typeparam>
    /// <typeparam name="TKey3">The type of the third id field.</typeparam>
    public struct CompositeId<TKey1, TKey2, TKey3>
        where TKey1 : struct
        where TKey2 : struct
        where TKey3 : struct
    {
        /// <summary>
        ///     The first key in the CompositeId.
        /// </summary>
        public readonly TKey1 Key1;

        /// <summary>
        ///     The second key in the CompositeId.
        /// </summary>
        public readonly TKey2 Key2;

        /// <summary>
        ///     The third key in the CompositeId.
        /// </summary>
        public readonly TKey3 Key3;

        /// <summary>
        ///     Creates a new CompositeId
        /// </summary>
        /// <param name="key1">The first id value.</param>
        /// <param name="key2">The second id value</param>
        /// <param name="key3">The third id value.</param>
        public CompositeId(TKey1 key1, TKey2 key2, TKey3 key3)
            : this()
        {
            Key1 = key1;
            Key2 = key2;
            Key3 = key3;
        }

        /// <summary>
        ///     Determines if this key is equal to another object based on the following rules:
        ///     <list type="number">
        ///         <item>
        ///             The other object must also be a CompositeId with the same number and type of parameters.
        ///         </item>
        ///         <item>
        ///             Each key value must considered equal to its counterpart per their <see cref="object.Equals(object)" />
        ///             methods.
        ///         </item>
        ///     </list>
        /// </summary>
        /// <param name="obj">The object to compare the composite key to.</param>
        /// <returns>True if the objects are equal. False otherwise.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is CompositeId<TKey1, TKey2, TKey3>)) return false;

            var other = (CompositeId<TKey1, TKey2, TKey3>)obj;
            return Key1.Equals(other.Key1) && Key2.Equals(other.Key2) && Key3.Equals(other.Key3);
        }

        /// <summary>
        ///     Produces a combined hash based on the values of the three key properties.
        /// </summary>
        /// <returns>A combined hash code.</returns>
        public override int GetHashCode()
        {
            // Get the first two key's hash codes
            var h1 = Key1.GetHashCode();
            var h2 = Key2.GetHashCode();

            // Combine the first two key's hash codes
            h1 = (h1 << 5) + h1 ^ h2;

            // Get the third key's hash code
            h2 = Key3.GetHashCode();

            // Combine the hash with the third key's hash code
            return (h1 << 5) + h1 ^ h2;
        }
    }
}