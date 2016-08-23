﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilitron.Functional;

namespace Utilitron.Test.Unit.Functional
{
    [TestClass]
    public class Union3Tests
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Item2_Throws_InvalicCastException_When_Set_To_Type_Of_Item1()
        {
            const int value = 100;
            var result = (Union<int, string, DateTime, object>)100;

            Assert.IsNotNull(result.Item);
            Assert.AreEqual(value, result.Item1);
            var item1 = result.Item2;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Item1_Throws_InvalicCastException_When_Set_To_Type_Of_Item2()
        {
            const string value = "thingy";
            var result = (Union<int, string, DateTime, object>)value;

            Assert.IsNotNull(result.Item);
            Assert.AreEqual(value, result.Item2);
            var item1 = result.Item1;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void Item1_Throws_InvalicCastException_When_Set_To_Type_Of_Item3()
        {
            var value = DateTime.MaxValue;
            var result = (Union<int, string, DateTime, object>)value;

            Assert.IsNotNull(result.Item);
            Assert.AreEqual(value, result.Item2);
            var item1 = result.Item1;
        }
    }
}
