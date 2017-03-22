﻿using System;
using System.CodeDom.Compiler;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilitron.Data;

namespace Utilitron.Tests.Data
{
    [TestClass]
    //[ExcludeFromCodeCoverage]
    [GeneratedCode("Testing", "")] // TODO: .NET Core won't have [ExcludeFromCodeCoverage] until 2.0 so this is temporary
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
    we should be selecting instead of *
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

        [TestMethod]
        public void GetEmbeddedQuery_Includes_Included_Queries()
        {
            const string newline = @"
";

            const string expected = @"before first include
/* Utilitron.Include: ./../RepositoryAncestor1Queries/IncludeQueryInner.sql */
include query
after first include
before second include
/* Utilitron.Include: /Utilitron/Tests/Data/RepositoryAncestor1Queries/IncludeQueryInner.sql */
include query
after second include";

            if (Environment.NewLine != newline) Assert.Inconclusive($"Environment.NewLine is not {string.Join(string.Empty, newline.Select(x => ((int) x).ToString("X4")))}.");

            var result = QueryUtilities.GetEmbeddedQuery("IncludeQueryOuter", typeof(RepositoryAncestor1));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetEmbeddedQuery_Throws_InvalidOperationException_If_Includes_Become_Recursive()
        {
            QueryUtilities.GetEmbeddedQuery("IncludeQueryRecursive", typeof(RepositoryAncestor1));
        }
    }
}
