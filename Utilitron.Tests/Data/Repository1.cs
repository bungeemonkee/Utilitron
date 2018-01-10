using System.CodeDom.Compiler;
using Utilitron.Data;

namespace Utilitron.Tests.Data
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class Repository1 : RepositoryAncestor
    {
        public Repository1(IRepositoryConfiguration configuration) : base(configuration)
        {
        }

        public string QueryTestProtected_Test()
        {
            return QueryTestProtected();
        }
    }
}