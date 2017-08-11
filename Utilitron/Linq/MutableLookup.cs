namespace Utilitron.Linq
{
    /// <summary>
    ///     See <see cref="IMutableLookup{TKey,TValue}" />.
    /// </summary>
    public class MutableLookup<TKey, TValue> : IMutableLookup<TKey, TValue>
    {
        private readonly IDictionary<TKey, MutableGrouping> _values;

        /// <summary>
        ///     Create a new MutableLookup with a default <see cref="EqualityComparer{TKey}" />.
        /// </summary>
        public MutableLookup()
            : this(EqualityComparer<TKey>.Default)
        {
        }

        /// <summary>
        ///     Create a new MutableLookup with the given <see cref="EqualityComparer{TKey}" />.
        /// </summary>
        /// <param name="keyComparer"></param>
        public MutableLookup(IEqualityComparer<TKey> keyComparer)
        {
            _values = new Dictionary<TKey, MutableGrouping>(keyComparer);
        }

        /// <summary>
        ///     See <see cref="ILookup{TKey,TElement}.Count" />.
        /// </summary>
        public int Count => _values.Count;

        /// <summary>
        ///     See <see cref="ILookup{TKey,TElement}.this" />.
        /// </summary>
        public IEnumerable<TValue> this[TKey key] => _values[key];

        /// <summary>
        ///     See <see cref="IMutableLookup{TKey,TValue}.Add(TKey,TValue)" />.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            MutableGrouping group;
            if (!_values.TryGetValue(key, out group))
            {
                group = new MutableGrouping(key);
                _values[key] = group;
            }
            group.Add(value);
        }

        /// <summary>
        ///     See <see cref="ILookup{TKey,TElement}.Contains(TKey)" />.
        /// </summary>
        public bool Contains(TKey key)
        {
            return _values.ContainsKey(key);
        }

        /// <summary>
        ///     See <see cref="IMutableLookup{TKey,TValue}.Clear()" />.
        /// </summary>
        public void Clear()
        {
            _values.Clear();
        }

        /// <summary>
        ///     See <see cref="IEnumerable{T}.GetEnumerator()" />.
        /// </summary>
        public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
        {
            return _values.Values.Cast<IGrouping<TKey, TValue>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct MutableGrouping : IGrouping<TKey, TValue>
        {
            private readonly IList<TValue> _values;

            public TKey Key { get; }

            public MutableGrouping(TKey key)
            {
                Key = key;

                _values = new List<TValue>();
            }

            public void Add(TValue value)
            {
                _values.Add(value);
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return _values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return _values.GetEnumerator();
            }
        }
    }
}