using System.Data.Common;

namespace Utilitron.Data.Profiling
{
    /// <summary>
    ///     Extension methods for profiling various database objects.
    /// </summary>
    public static class ProfilingExtensions
    {
        /// <summary>
        ///     Enables profiling for this <see cref="DbConnection" /> including any <see cref="DbTransaction" />s and
        ///     <see cref="DbCommand" />s created through it.
        /// </summary>
        /// <returns>A new <see cref="ProfilingConnection" /> for the given <see cref="DbConnection" />.</returns>
        public static ProfilingConnection Profile(this DbConnection connection)
        {
            return connection as ProfilingConnection ?? new ProfilingConnection(connection);
        }

        /// <summary>
        ///     Enables profiling for this <see cref="DbTransaction" />.
        /// </summary>
        /// <returns>A new <see cref="ProfilingTransaction" /> for the given <see cref="DbTransaction" />.</returns>
        public static ProfilingTransaction Profile(this DbTransaction transaction)
        {
            return transaction as ProfilingTransaction ?? new ProfilingTransaction(transaction);
        }

        /// <summary>
        ///     Enables profiling for this <see cref="DbCommand" />.
        /// </summary>
        /// <returns>A new <see cref="ProfilingCommand" /> for the given <see cref="DbCommand" />.</returns>
        public static ProfilingCommand Profile(this DbCommand command)
        {
            return command as ProfilingCommand ?? new ProfilingCommand(command);
        }
    }
}
