using System.Diagnostics.CodeAnalysis;
using Utilitron.Data;

namespace Utilitron.Test.Unit.Data
{
    [ExcludeFromCodeCoverage]
    public class ValidModelType : Model<int, ValidModelType>
    {
        public int Id { get; set; }

        public override int GetIdentifier() => Id;
    }
}
