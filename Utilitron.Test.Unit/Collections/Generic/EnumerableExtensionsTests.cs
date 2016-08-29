using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    }
}