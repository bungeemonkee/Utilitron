using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilitron.Collections.Generic;

namespace Utilitron.Test.Unit.Collections.Generic
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Partition_ArgumentNullException_When_Source_Null()
        {
            EnumerableExtensions.Partition<int>(null, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Partition_ArgumentOutOfRangeException_When_Parititons_Less_Than_One()
        {
            Enumerable.Empty<int>().Partition(0);
        }

        [TestMethod]
        public void Partition_List()
        {
            var input = new[] {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
            input.Partition(5);
        }

        [TestMethod]
        public void Partition_With_Non_List()
        {
            Enumerable.Repeat(1, 10).Partition(5);
        }

        [TestMethod]
        public void MinMax_Returns_Minimum_And_Maximum_Values()
        {
            var source = new[]
                         {
                             new TestObject
                             {
                                 TestProperty = 2
                             },
                             new TestObject
                             {
                                 TestProperty = 3
                             },
                             new TestObject
                             {
                                 TestProperty = -1
                             }
                         };

            var selectorMock = new Mock<Func<TestObject, int>>();

            Func<TestObject, int> selector = x => x.TestProperty;

            var result = source.MinMax(selector);

            Assert.AreEqual(-1, result.Min);
            Assert.AreEqual(3, result.Max);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MinMax_Throws_ArgumentNullException_For_Null_Comparer()
        {
            var sourceMock = new Mock<IEnumerable<TestObject>>();
            var selectorMock = new Mock<Func<TestObject, int>>();

            var result = sourceMock.Object.MinMax(selectorMock.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MinMax_Throws_ArgumentNullException_For_Null_Selector()
        {
            var sourceMock = new Mock<IEnumerable<TestObject>>();
            var comparerMock = new Mock<IComparer<int>>();

            var result = sourceMock.Object.MinMax(null, comparerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MinMax_Throws_ArgumentNullException_For_Null_Source()
        {
            var selectorMock = new Mock<Func<TestObject, int>>();
            var comparerMock = new Mock<IComparer<int>>();

            var result = EnumerableExtensions.MinMax(null, selectorMock.Object, comparerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MinMax_Throws_InvalidOperationException_For_Empty_Source()
        {
            var source = Enumerable.Empty<TestObject>();
            var selectorMock = new Mock<Func<TestObject, int>>();
            var comparerMock = new Mock<IComparer<int>>();

            var result = source.MinMax(selectorMock.Object, comparerMock.Object);
        }

        [TestMethod]
        public void MinMax_Uses_Custom_Comparer()
        {
            var source = new[]
                         {
                             new TestObject
                             {
                                 TestProperty = 2
                             },
                             new TestObject
                             {
                                 TestProperty = 3
                             },
                             new TestObject
                             {
                                 TestProperty = -1
                             }
                         };

            Func<TestObject, int> selector = x => x.TestProperty;

            var comparerMock = new Mock<IComparer<int>>();
            comparerMock.Setup(x => x.Compare(It.IsAny<int>(), It.IsAny<int>()))
                        .Returns((int x, int y) => x - y);

            var result = source.MinMax(selector, comparerMock.Object);

            Assert.AreEqual(-1, result.Min);
            Assert.AreEqual(3, result.Max);
            comparerMock.Verify(x => x.Compare(It.IsAny<int>(), It.IsAny<int>()));
        }
    }
}