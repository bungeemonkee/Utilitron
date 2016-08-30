using System.Collections.Generic;
using System.Linq;

namespace Utilitron.Data
{
    /// <summary>
    /// Extensions for any <see cref="IEnumerable{T}"/> of <see cref="Model{TIdentifier,TModel}"/> instances.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Produce a dictionary of models by id from any enumerable of models.
        /// </summary>
        /// <typeparam name="TIdentifier">The model identifier type.</typeparam>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <param name="models">The enumerable of models.</param>
        /// <returns>A dictionary of models by identifier.</returns>
        public static IDictionary<TIdentifier, TModel> ToDictionary<TIdentifier, TModel>(this IEnumerable<TModel> models)
            where TIdentifier : struct
            where TModel : Model<TIdentifier, TModel>
        {
            return models.ToDictionary(x => x.GetIdentifier());
        }
    }
}
