
namespace Utilitron.Data
{
    /// <summary>
    /// Static methods for <see cref="CompositeId{TKey1,TKey2}"/> and <see cref="CompositeId{TKey1,TKey2,TKey3}"/> instances.
    /// </summary>
    public static class CompositeId
    {
        /// <summary>
        /// Creates a new <see cref="CompositeId{TKey1,TKey2}"/> instance.
        /// </summary>
        public static CompositeId<TKey1, TKey2> Create<TKey1, TKey2>(TKey1 key1, TKey2 key2)
            where TKey1 : struct
            where TKey2 : struct
        {
            return new CompositeId<TKey1, TKey2>(key1, key2);
        }

        /// <summary>
        /// Creates a new <see cref="CompositeId{TKey1,TKey2,TKey3}"/> instance.
        /// </summary>
        public static CompositeId<TKey1, TKey2, TKey3> Create<TKey1, TKey2, TKey3>(TKey1 key1, TKey2 key2, TKey3 key3)
            where TKey1 : struct
            where TKey2 : struct
            where TKey3 : struct
        {
            return new CompositeId<TKey1, TKey2, TKey3>(key1, key2, key3);
        }
    }
}
