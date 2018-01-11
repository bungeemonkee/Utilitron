using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Utilitron.Data
{
    /// <summary>
    ///     Utilities for sql queries.
    /// </summary>
    public static class QueryUtilities
    {
        private const char Asterix = '*';
        private const char Dash = '-';
        private const char Slash = '/';

        private static readonly char[] IncludePathSplitters =
        {
            '/',
            '\\'
        };

        private static readonly Regex IncludeRegex = new Regex(@"/\*\s+Utilitron\.Include:\s+([0-9a-zA-Z/\\\.]+\.sql)\s+\*/");

        // See: https://msdn.microsoft.com/en-us/library/t809ektx(v=vs.110).aspx
        private static readonly char[] NewLines =
        {
            (char) 0x2028, // LINE SEPARATOR
            (char) 0x0009, // CHARACTER TABULATION
            (char) 0x000B, // LINE TABULATION
            (char) 0x000C, // FORM FEED
            (char) 0x0085, // NEXT LINE

            // Any characters in TwoCharNewLine must also be in this array
            (char) 0x000D, // CARRIAGE RETURN
            (char) 0x000A // LINE FEED
        };

        private static readonly char[] QueryNamePathSplitters =
        {
            '.'
        };

        private static readonly char[] TwoCharNewLine =
        {
            (char) 0x000D, // CARRIAGE RETURN
            (char) 0x000A // LINE FEED
        };

        /// <summary>
        ///     Get the given embedded query for the given repository type.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         The query name is expected to be the name of an embedded sql file.
        ///         The location of the query file is the same namespace as the repository type
        ///         plus the repository type name and the word "Queries".
        ///     </para>
        ///     <para>
        ///         If a query exists on a base class of the repository then the namespace of the query is expected to be the
        ///         namespace of the lowest-level base class that implements a method with that name. So if an abstract method is
        ///         defined (but has not method body) it will not be associated with the abstract class in which it is defined but
        ///         rather the lowest-level class in which it has an implementation (an actual method body). This also applies to
        ///         method overloads. If the method is overloaded and exists on more than one class then the lowest-level class
        ///         with an overload with that same method name will be the namespace that is used. If an overload is abstract then
        ///         the lowest level class that implements that overload will be the namespace used to find the query.
        ///     </para>
        ///     <para>
        ///         Example: A repository with a full name of "Utilitron.Example.Repository" and a query name of "Query"
        ///         would look for an embedded resource named "Utilitron.Example.RepositoryQueries.Query.sql".
        ///     </para>
        ///     <para>
        ///         The embedded queries support simple include directives of the form /* Utilitron.Include: [FilePath] */. These
        ///         included query files must also be embedded resources. These includes can be absolute or relative. The file
        ///         paths can include forward or backward slashes.
        ///         <list type="bullet">
        ///             <listheader>
        ///                 Include Directive Examples:
        ///             </listheader>
        ///             <item>
        ///                 /* Utilitron.Include: IncludeInSameFolder.sql */
        ///             </item>
        ///             <item>
        ///                 /* Utilitron.Include: ../IncludeInParentFolder.sql */
        ///             </item>
        ///             <item>
        ///                 /* Utilitron.Include: Includes/IncludeInSubfolder.sql */
        ///             </item>
        ///             <item>
        ///                 /* Utilitron.Include: /Utilitron/Data/Repository/Includes/IncludeInSubfolder.sql */
        ///             </item>
        ///         </list>
        ///     </para>
        ///     Note: This function is not cheap and does not cache its results. <see cref="Repository.GetQuery" /> uses this
        ///     method but does cache the results.
        /// </remarks>
        /// <param name="queryName">The name of the query to get.</param>
        /// <param name="repositoryType">The type of the repository requesting the query.</param>
        /// <returns>The query text.</returns>
        public static string GetEmbeddedQuery(string queryName, Type repositoryType)
        {
            const BindingFlags flags = BindingFlags.Public
                | BindingFlags.NonPublic
                | BindingFlags.Instance
                | BindingFlags.Static
                | BindingFlags.FlattenHierarchy;

            // Get the members that match this query name
            var members = repositoryType.GetMethods(flags)
                .Where(x => x.Name == queryName);

            // Get the distinct set of types those members are declared on
            var types = members.Select(x => x.DeclaringType)
                .Distinct()
                .ToList();

            if (types.Count == 1)
                repositoryType = types[0];
            else
            {
                var maxDepth = 0;
                var originalRepositoryType = repositoryType;

                // Find the type that is furthest down the inheritance tree of any type in the list
                foreach (var baseType in types)
                {
                    var depth = TypeUtility.GetTypeDepth(originalRepositoryType, baseType);
                    if (depth <= maxDepth)
                        continue;

                    repositoryType = baseType;
                    maxDepth = depth;
                }
            }

            // Add the type name (and "Queries") to the query name
            queryName = $"{repositoryType.FullName}Queries.{queryName}.sql";

            // Get the embedded resource from the assembly
            var assembly = repositoryType.GetTypeInfo()
                .Assembly;
            var query = GetEmbeddedQueryText(queryName, assembly);

            // Process any includes in the query
            query = ProcessQueryIncludes(query, queryName, assembly, Enumerable.Empty<string>());

            // Return the query
            return query;
        }

        /// <summary>
        ///     A simple query minification function.
        /// </summary>
        /// <remarks>
        ///     <list type="bullet">
        ///         <listheader>
        ///             This function does three things:
        ///         </listheader>
        ///         <item>
        ///             Remove duplicate newlines.
        ///         </item>
        ///         <item>
        ///             Remove leading whitespace.
        ///         </item>
        ///         <item>
        ///             Remove comments.
        ///         </item>
        ///     </list>
        ///     Note: This function is not cheap and does not cache its results.
        ///     Note: This function does not properly handle comments inside strings inside the queries.
        /// </remarks>
        /// <param name="query">The query to minify.</param>
        /// <returns>A minified version of the query.</returns>
        public static string Minify(string query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var sb = new StringBuilder(query.Length);

            var previousWasNewLine = true;
            var hasSeenNonWhitespace = false;
            var inSingleLineComment = false;
            var commentMarkerCount = 0;

            for (var i = 0; i < query.Length; ++i)
            {
                var previous = i != 0
                    ? query[i - 1]
                    : (char) 0;
                var current = query[i];
                var next = i + 1 < query.Length
                    ? query[i + 1]
                    : (char) 0;

                // Entering a new multiline comment?
                if (current == Slash && next == Asterix && !inSingleLineComment)
                {
                    ++i; // Comment markers are two characters so advance one more
                    ++commentMarkerCount;
                    continue;
                }

                // Exiting a multiline comment?
                if (commentMarkerCount > 0 && previous != Slash && current == Asterix && next == Slash)
                {
                    ++i; // Comment markers are two characters so advance one more
                    --commentMarkerCount;
                    continue;
                }

                // Skip to the end of the multiline comment
                if (commentMarkerCount > 0)
                    continue;

                // Entering a new single line comment?
                if (current == Dash && next == Dash)
                {
                    ++i; // Comment markers are two characters so advance one more
                    inSingleLineComment = true;
                    continue;
                }

                // Handle new lines (collapse duplicate newlines, end single line comments, reset previous whitespace)
                if (NewLines.Contains(current))
                {
                    // Exiting a single line comment
                    if (inSingleLineComment)
                        inSingleLineComment = false;

                    // If the only characters before were newlines (and/or whitespace) ignore this
                    if (previousWasNewLine)
                        continue;

                    // The previous character was (now) a newline and no non-whitespace has been seen on this line
                    previousWasNewLine = true;
                    hasSeenNonWhitespace = false;

                    // Append the newline (do this specially to handle two-character newlines)
                    if (current == TwoCharNewLine[0] && next == TwoCharNewLine[1])
                    {
                        ++i;
                        sb.Append(TwoCharNewLine);
                    }
                    else
                        sb.Append(current);

                    continue;
                }

                // If in a single line comment skip to the end
                if (inSingleLineComment)
                    continue;

                // Handle white space
                if (char.IsWhiteSpace(current))
                {
                    if (!hasSeenNonWhitespace)
                        continue;
                }
                else
                    hasSeenNonWhitespace = true;

                // This character is not a newline (or whitespace)
                previousWasNewLine = false;

                // Add the character
                sb.Append(current);
            }

            return sb.ToString();
        }

        private static string GetEmbeddedQueryText(string queryName, Assembly assembly)
        {
            // Underscores in embedded resource names get escaped to double underscores for some reason
            queryName = queryName.Replace("_", "__");

            using (var resource = assembly.GetManifestResourceStream(queryName))
            {
                if (resource == null)
                    throw new InvalidOperationException($"No embedded query for {queryName}.");

                using (var text = new StreamReader(resource, Encoding.UTF8, true))
                {
                    return text.ReadToEnd();
                }
            }
        }

        private static string ProcessQueryIncludeMatch(Match match, string parentQueryName, Assembly assembly, ISet<string> previousParents)
        {
            // Add the parent to the previous parents collection
            previousParents.Add(parentQueryName);

            // Get the location of the embedded include
            var includePath = match.Groups[1]
                .Value;

            // If the include path is "relative" then process it against the parent query's embedded resource location
            if (IncludePathSplitters.Any(x => includePath[0] == x))
            {
                // The include is an "absolute" embedded resource

                // Trim any starting slashes
                includePath = includePath.Trim(IncludePathSplitters);

                // Replace any slashes with periods
                includePath = IncludePathSplitters.Aggregate(includePath, (x, y) => x.Replace(y, '.'));
            }
            else
            {
                // The include is a "relative" embedded resource

                // Get the absolute name of the embedded include
                var parentQueryNameParts = parentQueryName.Split(QueryNamePathSplitters, StringSplitOptions.RemoveEmptyEntries);
                var includePathParts = includePath.Split(IncludePathSplitters, StringSplitOptions.RemoveEmptyEntries);
                var includePathBuilder = new List<string>(parentQueryNameParts.Length + includePathParts.Length);
                includePathBuilder.AddRange(parentQueryNameParts.Take(parentQueryNameParts.Length - 2));
                foreach (var part in includePathParts)
                {
                    switch (part)
                    {
                        case ".":
                            continue;
                        case "..":
                            includePathBuilder.RemoveAt(includePathBuilder.Count - 1);
                            break;
                        default:
                            includePathBuilder.Add(part);
                            break;
                    }
                }

                includePath = string.Join(".", includePathBuilder);
            }

            // Make sure the include is not already a parent
            if (previousParents.Contains(includePath))
                throw new InvalidOperationException($"Recursive embedded query include: {includePath}");

            // Get the include from the embedded resource path
            var query = GetEmbeddedQueryText(includePath, assembly);

            // Process includes in the new included query (here's the recursive bit)
            query = ProcessQueryIncludes(query, includePath, assembly, previousParents);

            // Add the include text back to the top of the included query
            query = match.Value + Environment.NewLine + query;

            // Return the include query
            return query;
        }

        private static string ProcessQueryIncludes(string query, string queryName, Assembly assembly, IEnumerable<string> previousParents)
        {
            // Process any include statements in the query
            return IncludeRegex.Replace(query, match => ProcessQueryIncludeMatch(match, queryName, assembly, new HashSet<string>(previousParents)));
        }
    }
}
