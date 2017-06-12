using System.CodeDom.Compiler;
using Utilitron.Data;

namespace Utilitron.Tests.Data._
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class UnderscoreRepository : Repository
    {
        public UnderscoreRepository(IRepositoryConfiguration configuration) : base(configuration)
        {
        }

        public string UnderscoreTest()
        {
            return GetQuery();
        }
    }
}