using System.CodeDom.Compiler;
using Utilitron.Data;

namespace Utilitron.Tests.Data
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class Repository2: RepositoryAbstract
    {
        public Repository2(IRepositoryConfiguration configuration)
            : base(configuration)
        {
        }

        public override string QueryTestAbstract()
        {
            return GetQuery();
        }
    }
}