using System;
using System.Linq;
using System.Text;

namespace Utilitron.Data
{
    /// <summary>
    /// Utilities for sql queries.
    /// </summary>
    public static class QueryUtilities
    {
        private const char Slash = '/';
        private const char Asterix = '*';
        private const char Dash = '-';

        // See: https://msdn.microsoft.com/en-us/library/t809ektx(v=vs.110).aspx
        private static readonly char[] NewLines =
        {
            '\u2028', // LINE SEPARATOR
            '\u0009', // CHARACTER TABULATION
            '\u000B', // LINE TABULATION
            '\u000C', // FORM FEED
            '\u0085', // NEXT LINE
        };

        private static readonly char[] TwoCharNewLine =
        {
            '\u000D', // CARRIAGE RETURN
            '\u000A', // LINE FEED
        };

        /// <summary>
        /// A simple query minification function.
        /// </summary>
        /// <remarks>
        /// <list type="bullet">
        /// <listheader>
        /// This function does three things:
        /// </listheader>
        /// <item>
        /// Remove duplicate newlines.
        /// </item>
        /// <item>
        /// Remove leading whitespace.
        /// </item>
        /// <item>
        /// Remove comments.
        /// </item>
        /// </list>
        /// Note: This function is not cheap and does not cache its results.
        /// Note: This function does not properly handle comments inside strings inside the queries.
        /// </remarks>
        /// <param name="query">The query to minify.</param>
        /// <returns>A minified version of the query.</returns>
        public static string Minify(string query)
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var sb = new StringBuilder(query.Length);

            var previousWasNewLine = true;
            var hasSeenNonWhitespace = false;
            var inSingleLineComment = false;
            var commentMarkerCount = 0;

            for (var i = 0; i < query.Length; ++i)
            {
                var previous = i != 0
                    ? query[i - 1]
                    : (char)0;
                var current = query[i];
                var next = i + 1 < query.Length
                    ? query[i + 1]
                    : (char)0;

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
                if (commentMarkerCount > 0) continue;

                // Entering a new single line comment?
                if (current == Dash && next == Dash)
                {
                    ++i; // Comment markers are two characters so advance one more
                    inSingleLineComment = true;
                    continue;
                }

                // Handle new lines (collapse duplicate newlines, end single line comments, reset previous whitespace)
                if (NewLines.Contains(current) || (current == TwoCharNewLine[0] && next == TwoCharNewLine[1]))
                {
                    // Exiting a single line comment
                    if (inSingleLineComment)
                    {
                        inSingleLineComment = false;
                    }

                    // If the only characters before were newlines (and/or whitespace) ignore this
                    if (previousWasNewLine) continue;

                    // The previous character was (now) a newline and no non-whitespace has been seen on this line
                    previousWasNewLine = true;
                    hasSeenNonWhitespace = false;

                    // Append the newline (do this specially to handle two-character newlines)
                    if (current == TwoCharNewLine[0])
                    {
                        ++i;
                        sb.Append(TwoCharNewLine);
                    }
                    else
                    {
                        sb.Append(current);
                    }

                    continue;
                }

                // If in a single line comment skip to the end
                if (inSingleLineComment) continue;

                // Handle white space
                if (char.IsWhiteSpace(current))
                {
                    if (!hasSeenNonWhitespace) continue;
                }
                else
                {
                    hasSeenNonWhitespace = true;
                }

                // This character is not a newline (or whitespace)
                previousWasNewLine = false;

                // Add the character
                sb.Append(current);
            }

            return sb.ToString();
        }
    }
}
