using System.Diagnostics.CodeAnalysis;
using Utilitron.Data;

namespace Utilitron.Tests.Unit.Data
{
    [ExcludeFromCodeCoverage]
    public class InvalidModelType : Model<int, ValidModelType>
    {
        public int Id { get; set; }

        public override int GetIdentifier() => Id;
    }
}
