using System;

namespace Utilitron.Data.Profiling
{
    public class ProfilingEventEndArgs<T>: ProfilingEventStartArgs<T>
    {
        public readonly DateTimeOffset EndTime;
        public readonly Exception Exception;

        public TimeSpan Duration => EndTime - StartTime;

        public bool Success => Exception == null;

        public ProfilingEventEndArgs(T item, DateTimeOffset startTime, DateTimeOffset endTime)
            : this(item, startTime, endTime, null)
        {
        }

        public ProfilingEventEndArgs(T item, DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
            : base(item, startTime)
        {
            EndTime = endTime;
            Exception = exception;
        }
    }
}
