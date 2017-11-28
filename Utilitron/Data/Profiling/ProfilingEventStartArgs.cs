using System;

namespace Utilitron.Data.Profiling
{
    public class ProfilingEventStartArgs<T>: EventArgs
    {
        public readonly T Item;

        public readonly DateTimeOffset StartTime;

        public ProfilingEventStartArgs(T item, DateTimeOffset startTime)
        {
            Item = item;
            StartTime = startTime;
        }
    }
}
