using System.Collections.Generic;

namespace Utilitron.Data
{
    /// <summary>
    /// Extensions for dictionaries that operate on models.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Add a model to a dictionary or return the equivalent reference to the model from the dictionary.
        /// </summary>
        /// <typeparam name="TIdentifier">The model's identifier type.</typeparam>
        /// <typeparam name="TModel">The model type.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="model">The model.</param>
        /// <returns>The model (after being added) or the reference already in the dictionary.</returns>
        public static TModel GetOrAdd<TIdentifier, TModel>(this IDictionary<TIdentifier, TModel> dictionary, TModel model)
            where TIdentifier : struct
            where TModel : Model<TIdentifier, TModel>
        {
            var id = model.GetIdentifier();

            TModel result;
            if (dictionary.TryGetValue(id, out result)) return result;

            dictionary.Add(id, model);

            return model;
        }
    }
}
