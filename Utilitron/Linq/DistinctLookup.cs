using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Linq
{
    /// <summary>
    ///     See <see cref="IDistinctLookup{TKey,TValue}" />.
    /// </summary>
    public class DistinctLookup<TKey, TValue> : IDistinctLookup<TKey, TValue>
    {
        private readonly IEqualityComparer<TValue> _valueComparer;
        private readonly IDictionary<TKey, DistinctGrouping> _values;

        /// <summary>
        ///     Create a new DistinctLookup with a default <see cref="EqualityComparer{TKey}" /> and
        ///     <see cref="EqualityComparer{TValue}" />.
        /// </summary>
        public DistinctLookup()
            : this(EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default)
        {
        }

        /// <summary>
        ///     Create a new DistinctLookup with the given <see cref="EqualityComparer{TKey}" /> and
        ///     <see cref="EqualityComparer{TValue}" />.
        /// </summary>
        /// <param name="keyComparer"></param>
        /// <param name="valueComparer"></param>
        public DistinctLookup(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            _values = new Dictionary<TKey, DistinctGrouping>(keyComparer);
            _valueComparer = valueComparer;
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
            DistinctGrouping group;
            if (!_values.TryGetValue(key, out group))
            {
                group = new DistinctGrouping(key, _valueComparer);
                _values[key] = group;
            }
            group.Add(value);
        }

        /// <summary>
        ///     See <see cref="IDistinctLookup{TKey,TValue}" />.
        /// </summary>
        public bool Contains(TKey key, TValue value)
        {
            DistinctGrouping group;
            return _values.TryGetValue(key, out group) && group.Contains(value);
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
        public IEnumerator<IDistinctGrouping<TKey, TValue>> GetEnumerator()
        {
            return _values.Values.Cast<IDistinctGrouping<TKey, TValue>>().GetEnumerator();
        }

        IEnumerator<IGrouping<TKey, TValue>> IEnumerable<IGrouping<TKey, TValue>>.GetEnumerator()
        {
            return _values.Values.Cast<IGrouping<TKey, TValue>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private struct DistinctGrouping : IDistinctGrouping<TKey, TValue>
        {
            private readonly HashSet<TValue> _values;

            public TKey Key { get; }

            public DistinctGrouping(TKey key, IEqualityComparer<TValue> valueComparer)
            {
                Key = key;

                _values = new HashSet<TValue>(valueComparer);
            }

            public void Add(TValue value)
            {
                _values.Add(value);
            }

            public bool Contains(TValue value)
            {
                return _values.Contains(value);
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