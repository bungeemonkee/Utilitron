using System;
using System.IO;

namespace Utilitron.IO
{
    /// <summary>
    ///     Extension methods for all <see cref="TextWriter" />s.
    /// </summary>
    public static class TextWriterExtensions
    {
        /// <summary>
        ///     Write an exception to a <see cref="TextWriter" /> in a useful and human-readable format.
        /// </summary>
        /// <remarks>
        ///     This function if primarily intended for logging exceptions to conosle output or log files for developer use.
        ///     It is not intended to generate output for users.
        /// </remarks>
        /// <param name="out">The <see cref="TextWriter" /> to write the exception to.</param>
        /// <param name="exception">The exception to write.</param>
        public static void WriteException(this TextWriter @out, Exception exception)
        {
            while (exception != null)
            {
                @out.WriteLine($"EXCEPTION: {exception.GetType().Name} >>> {exception.Message.GetFirstLine()}");
                var aggregate = exception as AggregateException;
                if (aggregate != null)
                {
                    @out.WriteLine("  Inner Exceptions:");
                    foreach (var inner in aggregate.InnerExceptions)
                    {
                        @out.WriteLine($"  * {inner.GetType().Name} >>> {inner.Message.GetFirstLine()}");
                    }
                }
                @out.WriteLine(exception.StackTrace);

                // Get the next exception
                exception = exception.InnerException;
                if (exception == null) continue;
                @out.WriteLine();
                @out.WriteLine("INNER EXCEPTION >>> ------------------------------------------------------------");
            }
        }
    }
}