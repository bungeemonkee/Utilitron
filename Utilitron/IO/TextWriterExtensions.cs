using System;
using System.IO;

namespace Utilitron.IO
{
    /// <summary>
    /// Extension methods for all <see cref="TextWriter"/>s.
    /// </summary>
    public static class TextWriterExtensions
    {
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
