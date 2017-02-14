using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using Utilitron.Data;

namespace Utilitron.Tests.Unit.Data
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class ValidModelType : Model<int, ValidModelType>
    {
        public int Id { get; set; }

        public override int GetIdentifier() => Id;
    }
}
