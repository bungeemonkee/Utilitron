using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     See <see cref="IMutableLookup{TKey,TValue}" />.
    /// </summary>
    public class MutableLookup<TKey, TValue> : IMutableLookup<TKey, TValue>
    {
        private readonly IDictionary<TKey, IGrouping<TKey, TValue>> _values;

        /// <summary>
        ///     See <see cref="ILookup{TKey,TElement}.Count" />.
        /// </summary>
        public int Count => _values.Count;

        /// <summary>
        ///     See <see cref="ILookup{TKey,TElement}.this" />.
        /// </summary>
        public IEnumerable<TValue> this[TKey key] => _values[key];

        /// <summary>
        ///     Create a new MutableLookup with a default <see cref="EqualityComparer{T}" />.
        /// </summary>
        public MutableLookup()
            : this(EqualityComparer<TKey>.Default) { }

        /// <summary>
        ///     Create a new MutableLookup with the given <see cref="EqualityComparer{T}" />.
        /// </summary>
        /// <param name="keyComparer"></param>
        public MutableLookup(IEqualityComparer<TKey> keyComparer)
        {
            _values = new Dictionary<TKey, IGrouping<TKey, TValue>>(keyComparer);
        }

        /// <summary>
        ///     See <see cref="IMutableLookup{TKey,TValue}.Add(TKey,TValue)" />.
        /// </summary>
        public void Add(TKey key, TValue value)
        {
            IGrouping<TKey, TValue> group;
            if (!_values.TryGetValue(key, out group))
            {
                group = new MutableGrouping(key);
                _values[key] = group;
            }
            ((MutableGrouping)group).Add(value);
        }

        /// <summary>
        ///     See <see cref="ILookup{TKey,TElement}.Contains(TKey)" />.
        /// </summary>
        public bool Contains(TKey key)
        {
            return _values.ContainsKey(key);
        }

        /// <summary>
        ///     See <see cref="IEnumerable{T}.GetEnumerator()" />.
        /// </summary>
        public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
        {
            return _values.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _values.Values.GetEnumerator();
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
