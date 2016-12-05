using System.Diagnostics.CodeAnalysis;
using Utilitron.Data;

namespace Utilitron.Tests.Unit.Data
{
    [ExcludeFromCodeCoverage]
    public class RepositoryAncestor2 : RepositoryAncestor1
    {
        public RepositoryAncestor2(IRepositoryConfiguration configuration) : base(configuration)
        {
        }
    }
}