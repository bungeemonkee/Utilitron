using System.CodeDom.Compiler;
using Utilitron.Data;

namespace Utilitron.Tests.Data
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class InvalidModelType : Model<int, ValidModelType>
    {
        public int Id { get; set; }

        public override int GetIdentifier() => Id;
    }
}
