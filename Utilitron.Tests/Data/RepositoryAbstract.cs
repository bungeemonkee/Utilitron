using System.CodeDom.Compiler;
using System.Data;
using System.Threading.Tasks;
using Utilitron.Data;

namespace Utilitron.Tests.Data
{
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public abstract class RepositoryAbstract : Repository
    {
        protected RepositoryAbstract(IRepositoryConfiguration configuration) : base(configuration)
        {
        }

        public string QueryTest()
        {
            return GetQuery();
        }

        public abstract string QueryTestAbstract();

        protected override async Task<IDbConnection> GetConnectionAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}