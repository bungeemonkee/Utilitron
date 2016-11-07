using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilitron.Collections.Generic;

namespace Utilitron.Test.Unit.Collections.Generic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DictionaryExtensionsTests
    {
        [TestMethod]
        public void GetOrDefault_Returns_Default_If_Key_Not_In_Dictionary()
        {
            var key = "key";
            var value = "value";

            var dictionaryMock = new Mock<IDictionary<string, string>>();
            dictionaryMock.Setup(x => x.TryGetValue(key, out value))
                          .Returns(false);

            var result = dictionaryMock.Object.GetOrDefault(key);

            Assert.IsNull(result);
            dictionaryMock.Verify(x => x.TryGetValue(key, out value));
        }

        [TestMethod]
        public void GetOrDefault_Returns_Value_If_Key_In_Dictionary()
        {
            var key = "key";
            var value = "value";

            var dictionaryMock = new Mock<IDictionary<string, string>>();
            dictionaryMock.Setup(x => x.TryGetValue(key, out value))
                          .Returns(true);

            var result = dictionaryMock.Object.GetOrDefault(key);

            Assert.AreSame(result, value);
            dictionaryMock.Verify(x => x.TryGetValue(key, out value));
        }
    }
}
