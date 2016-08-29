namespace Utilitron.Functional
{
    /// <summary>
    ///     Represents the active item in a <see cref="Union{T1,T2,T3}" /> type.
    /// </summary>
    public enum UnionItem : byte
    {
        /// <summary>
        ///     The first item in the union is the active/real item.
        /// </summary>
        Item1 = 1,

        /// <summary>
        ///     The second item in the union is the active/real item.
        /// </summary>
        Item2 = 2,

        /// <summary>
        ///     The third item in the union is the active/real item.
        /// </summary>
        Item3 = 3
    }
}