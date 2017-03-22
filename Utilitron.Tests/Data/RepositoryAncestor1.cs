using System.CodeDom.Compiler;
using Utilitron.Data;

namespace Utilitron.Tests.Data
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class RepositoryAncestor1 : Repository
    {
        public RepositoryAncestor1(IRepositoryConfiguration configuration) : base(configuration)
        {
        }

        public string QueryTest()
        {
            return GetQuery();
        }
    }
}