using Utilitron.Data;

namespace Utilitron.Test.Unit.Data
{
    public class RepositoryAncestor1 : Repository
    {
        public string QueryTest()
        {
            return GetQuery();
        }

        public RepositoryAncestor1(IRepositoryConfiguration configuration) : base(configuration)
        {
        }
    }
}
