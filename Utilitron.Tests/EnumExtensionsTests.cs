using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.CodeDom.Compiler;

namespace Utilitron.Tests
{
    [TestClass]
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
    public class EnumExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetDescription_Throws_ArgumentNullException()
        {
            EnumExtensions.GetDescription(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetDescription_Throws_ArgumentException()
        {
            EnumExtensions.GetDescription((TestEnum)10);
        }

        [TestMethod]
        public void GetDescription_Returns_Description()
        {
            const string expected = "First Value";

            var result = TestEnum.FirstValue.GetDescription();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetDescription_Returns_Field_Name_As_Default()
        {
            const string expected = "SecondValue";

            var result = TestEnum.SecondValue.GetDescription();

            Assert.AreEqual(expected, result);
        }
    }
}
