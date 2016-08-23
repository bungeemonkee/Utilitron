using System;

namespace Utilitron.Functional
{
    /// <summary>
    /// Simulate a union between three types.
    /// </summary>
    /// <typeparam name="T1">The first type in the union.</typeparam>
    /// <typeparam name="T2">The second type in the union.</typeparam>
    /// <typeparam name="T3">The third type in the union.</typeparam>
    /// <typeparam name="TBase">The shared base type.</typeparam>
    public struct Union<T1, T2, T3, TBase>
        where T1 : TBase
        where T2 : TBase
        where T3 : TBase
    {
        /// <summary>
        /// Which item in this union is active.
        /// </summary>
        public readonly UnionItem ActiveItem;

        /// <summary>
        /// The item as the base type.
        /// </summary>
        public readonly TBase Item;

        /// <summary>
        /// The item as the first type.
        /// </summary>
        public T1 Item1
        {
            get
            {
                if (ActiveItem != UnionItem.Item1)
                {
                    throw new InvalidCastException();
                }

                return (T1)Item;
            }
        }

        /// <summary>
        /// The item as the second type.
        /// </summary>
        public T2 Item2
        {
            get
            {
                if (ActiveItem != UnionItem.Item2)
                {
                    throw new InvalidCastException();
                }

                return (T2)Item;
            }
        }

        /// <summary>
        /// The item as the third type.
        /// </summary>
        public T3 Item3
        {
            get
            {
                if (ActiveItem != UnionItem.Item3)
                {
                    throw new InvalidCastException();
                }

                return (T3)Item;
            }
        }

        /// <summary>
        /// Create a union who's actual value is of the first type.
        /// </summary>
        /// <param name="item">The value.</param>
        public Union(T1 item)
            : this()
        {
            ActiveItem = UnionItem.Item1;
            Item = item;
        }

        /// <summary>
        /// Create a union who's actual value is of the second type.
        /// </summary>
        /// <param name="item">The value.</param>
        public Union(T2 item)
            : this()
        {
            ActiveItem = UnionItem.Item2;
            Item = item;
        }

        /// <summary>
        /// Create a union who's actual value is of the third type.
        /// </summary>
        /// <param name="item">The value.</param>
        public Union(T3 item)
            : this()
        {
            ActiveItem = UnionItem.Item3;
            Item = item;
        }

        /// <summary>
        /// Convert an item of the first type to the union type.
        /// </summary>
        /// <param name="item">The item.</param>
        public static implicit operator Union<T1, T2, T3, TBase>(T1 item)
        {
            return new Union<T1, T2, T3, TBase>(item);
        }

        /// <summary>
        /// Convert an item of the second type to the union type.
        /// </summary>
        /// <param name="item">The item.</param>
        public static implicit operator Union<T1, T2, T3, TBase>(T2 item)
        {
            return new Union<T1, T2, T3, TBase>(item);
        }

        /// <summary>
        /// Convert an item of the third type to the union type.
        /// </summary>
        /// <param name="item">The item.</param>
        public static implicit operator Union<T1, T2, T3, TBase>(T3 item)
        {
            return new Union<T1, T2, T3, TBase>(item);
        }

        /// <summary>
        /// Convert the union type to the base type.
        /// </summary>
        /// <param name="union">The union.</param>
        public static implicit operator TBase(Union<T1, T2, T3, TBase> union)
        {
            return union.Item;
        }

        /// <summary>
        /// Convert the union type to the first type.
        /// </summary>
        /// <param name="union">The union.</param>
        public static explicit operator T1(Union<T1, T2, T3, TBase> union)
        {
            return union.Item1;
        }

        /// <summary>
        /// Convert the union type to the second type.
        /// </summary>
        /// <param name="union">The union.</param>
        public static explicit operator T2(Union<T1, T2, T3, TBase> union)
        {
            return union.Item2;
        }

        /// <summary>
        /// Convert the union type to the third type.
        /// </summary>
        /// <param name="union">The union.</param>
        public static explicit operator T3(Union<T1, T2, T3, TBase> union)
        {
            return union.Item3;
        }
    }
}
