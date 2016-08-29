using System;

namespace Utilitron
{
    /// <summary>
    ///     Extensions for all strings.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly string[] Newlines = {"\r\n", "\n"};

        /// <summary>
        ///     Gets just the first line of a string (after any empty lines).
        /// </summary>
        public static string GetFirstLine(this string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text));
            }

            var lines = text.Split(Newlines, StringSplitOptions.RemoveEmptyEntries);
            return lines.Length == 0 ? null : lines[0];
        }
    }
}