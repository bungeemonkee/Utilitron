using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Utilitron.Test.Unit.Data
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Constructor_Throws_InvalidOperationException_When_TModel_Is_Not_The_Subclass()
        {
            var result = new InvalidModelType();
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Same_Reference_Is_Equal()
        {
            var ref1 = new ValidModelType();
            var ref2 = ref1;

            Assert.AreEqual(ref1, ref2);
        }

        [TestMethod]
        public void Models_With_Default_Identifiers_Are_Unequal()
        {
            var ref1 = new ValidModelType();
            var ref2 = new ValidModelType();

            Assert.AreNotEqual(ref1, ref2);
        }

        [TestMethod]
        public void Models_With_Equal_Non_Default_Identifiers_Are_Eequal()
        {
            var ref1 = new ValidModelType { Id = 1 };
            var ref2 = new ValidModelType { Id = 1 };

            Assert.AreEqual(ref1, ref2);
        }
    }
}
