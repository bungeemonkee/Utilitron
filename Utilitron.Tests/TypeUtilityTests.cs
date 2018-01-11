using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilitron.Tests
{
    [TestClass]
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class TypeUtilityTests
    {
        [TestMethod]
        public void Types_Are_Same()
        {
            var subType = typeof(object);
            var baseType = typeof(object);
            const int expected = 0;

            var result = TypeUtility.GetTypeDepth(subType, baseType);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Types_Are_Unrelated()
        {
            var subType = typeof(string);
            var baseType = typeof(int);
            const int expected = -1;

            var result = TypeUtility.GetTypeDepth(subType, baseType);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Type_Distance_1()
        {
            var subType = typeof(string);
            var baseType = typeof(object);
            const int expected = 1;

            var result = TypeUtility.GetTypeDepth(subType, baseType);
            Assert.AreEqual(expected, result);
        }
    }
}