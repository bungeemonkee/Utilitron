using System;

namespace Utilitron.Data
{
    /// <summary>
    ///     Base class for all data model classes.
    /// </summary>
    /// <typeparam name="TIdentifier">The subclass' identifier type.</typeparam>
    /// <typeparam name="TModel">The type of the subclass.</typeparam>
    public abstract class Model<TIdentifier, TModel>
        where TIdentifier : struct
        where TModel : Model<TIdentifier, TModel>
    {
        private static readonly TIdentifier IdentifierDefault = default(TIdentifier);

        private readonly bool _isTypeCheked;

        /// <summary>
        ///     Creates a new instance of the Model and ensures that it inherits from TModel.
        /// </summary>
        protected Model()
        {
            if (_isTypeCheked) return;

            if (!(this is TModel))
            {
                var type = GetType().FullName;
                var model = typeof(TModel).FullName;
                throw new InvalidOperationException($"{type} must inherit from {model}");
            }

            _isTypeCheked = true;
        }

        /// <summary>
        ///     Get the identifier for this model.
        ///     This method may be called many times and must perform quickly.
        /// </summary>
        /// <returns>This model's identifier.</returns>
        public abstract TIdentifier GetIdentifier();

        /// <summary>
        ///     Overrides <see cref="object.Equals(object)" /> to determine equality using the
        ///     <see cref="Model{TIdentifier,TModel}.GetIdentifier()" /> method.
        /// </summary>
        public override bool Equals(object obj)
        {
            // If they are the same reference they are always equal
            // ReSharper disable once BaseObjectEqualsIsObjectEquals
            if (base.Equals(obj)) return true;

            // if the other object is not this type they cannot be equal
            var other = obj as TModel;
            if (other == null) return false;

            // If either id is 0 they are to be considered unequal (unless they are the same reference)
            // This is because no db-generated id should be 0.
            // Which means that the two models are as yet unsaved.
            // So we consider them unequal because when they are each saved they will have different ids.
            if (GetIdentifier().Equals(IdentifierDefault) || other.GetIdentifier().Equals(IdentifierDefault)) return false;

            // They are equal if their ids are equal
            return GetIdentifier().Equals(other.GetIdentifier());
        }

        /// <summary>
        ///     Overrides <see cref="object.GetHashCode()" /> to use the hash code of the id value.
        /// </summary>
        public override int GetHashCode()
        {
            return GetIdentifier()
                .GetHashCode();
        }
    }
}
