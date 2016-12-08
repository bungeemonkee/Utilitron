using System.Data;
using System.Data.Common;

namespace Utilitron.Data
{
    /// <summary>
    ///     Defines configuration necessary for any database context.
    /// </summary>
    public interface IRepositoryConfiguration
    {
        /// <summary>
        ///     The connection string for the database.
        /// </summary>
        string RepositoryConnectionString { get; }

        /// <summary>
        ///     The default amount of time (in seconds) that a database command will run.
        /// </summary>
        int DefaultCommandTimeoutSeconds { get; }

        /// <summary>
        ///     The default isolation level of each transaction.
        /// </summary>
        IsolationLevel TransactionIsolationLevel { get; }
    }
}