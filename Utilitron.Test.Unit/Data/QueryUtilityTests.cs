using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilitron.Data;

namespace Utilitron.Test.Unit.Data
{
    [TestClass]
    public class QueryUtilityTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Minify_Throws_ArgumentNullException_If_Query_Is_Null()
        {
            QueryUtilities.Minify(null);
        }

        [TestMethod]
        public void Minify_Test_Simple()
        {
            const string query = @"

select *
/*
    This is a list of all the things
    we should be selecting instead of *
    It is also a multiline comment
*/
from Bananas
    inner join Apples
        -- This next bit is not valid syntax
        on stuff


";
            const string expected = @"select *
from Bananas
inner join Apples
on stuff
";
            var result = QueryUtilities.Minify(query);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Minify_Handles_Nested_Multiline_Comment()
        {
            const string query = @"
select *
/*
    This is a list of all the things
    we should be slecting instead of *
    It is also a multiline comment
    /*
        This is a multiline comment inside a multiline comment.
    */
*/
from Bananas
";
            const string expected = @"select *
from Bananas
";
            var result = QueryUtilities.Minify(query);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Minify_Handles_Unicode_Newline()
        {
            const string query = "select *\u2028\u2028\u2028from Bananas";
            const string expected = "select *\u2028from Bananas";
            var result = QueryUtilities.Minify(query);
            Assert.AreEqual(expected, result);
        }
    }
}
