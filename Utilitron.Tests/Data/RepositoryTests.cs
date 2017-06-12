using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilitron.Data;
using Utilitron.Tests.Data._;

namespace Utilitron.Tests.Data
{
    [TestClass]
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class RepositoryTests
    {
        [TestMethod]
        public void GetQuery_Returns_Query_From_Base_Repository_Class()
        {
            const string expected = "QueryTest";

            var configMock = new Mock<IRepositoryConfiguration>();

            var repo = new RepositoryAncestor2(configMock.Object);

            var result = repo.QueryTest();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetQuery_Returns_Query_From_Repository_With_Underscore_In_Namespace()
        {
            const string expected = "UnderscoreTest";

            var configMock = new Mock<IRepositoryConfiguration>();

            var repo = new UnderscoreRepository(configMock.Object);

            var result = repo.UnderscoreTest();

            Assert.AreEqual(expected, result);
        }
    }
}