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

        /// <summary>
        ///     Creates a new instance of the Model and ensures that it inherits from TModel.
        /// </summary>
        protected Model()
        {
            if (this is TModel) return;

            var type = GetType().FullName;
            var model = typeof(TModel).FullName;
            throw new InvalidOperationException($"'{type}' must inherit from '{model}'. Specify '{type}' as the 'TModel' parameter of 'Model<TIdentifier, TModel>'.");
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
        /// <remarks>
        ///     Equality is determined according to the following rules.
        ///     <list type="number">
        ///         <listheader>
        ///             These are my "3 Laws of Identifier Equality".
        ///         </listheader>
        ///         <item>
        ///             A model must always be equal to itself (the same pointer/reference) and models must be of the same type to be equal.
        ///         </item>
        ///         <item>
        ///             A model's identifier must not be equal to its identifier type's default value to be equal to another model, except
        ///             in cases where this would violate the first law.
        ///         </item>
        ///         <item>
        ///             A model's identifier must be equal to another model's identifier for thoe models to be equal, except in cases where this would violate
        ///             the first or second laws.
        ///         </item>
        ///     </list>
        /// </remarks>
        public override bool Equals(object obj)
        {
            // If they are the same reference they are always equal (Law #1)
            // ReSharper disable once BaseObjectEqualsIsObjectEquals
            if (base.Equals(obj)) return true;

            // If the other object is not this type they cannot be equal (Law #1)
            var other = obj as TModel;
            if (other == null) return false;

            // If either id is 0 they are to be considered unequal (unless they are the same reference) (Law #2)
            // This is because no db-generated id should be 0.
            // Which means that at least one of the two models is as yet unsaved.
            // So we consider them unequal because when saved they will have different ids.
            if (GetIdentifier().Equals(IdentifierDefault) || other.GetIdentifier().Equals(IdentifierDefault)) return false;

            // They are equal if their ids are equal (Law #3)
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