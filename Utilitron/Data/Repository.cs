using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Utilitron.Data
{
    /// <summary>
    ///     A base class for any Dapper based repositories.
    /// </summary>
    public abstract class Repository
    {
        private static readonly ConcurrentDictionary<string, string> Queries =
            new ConcurrentDictionary<string, string>();

        /// <summary>
        ///     The <see cref="IRepositoryConfiguration" /> for this repository.
        /// </summary>
        protected readonly IRepositoryConfiguration Configuration;

        /// <summary>
        ///     Create a new base repository with the given <see cref="IRepositoryConfiguration" />.
        /// </summary>
        /// <param name="configuration"></param>
        protected Repository(IRepositoryConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        ///     Execute a function using a connection.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="function">The function to execute.</param>
        /// <returns>The result of the function.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<DbConnection, Task<T>> function)
        {
            using (var connection = await GetConnectionAsync())
            {
                return await function(connection);
            }
        }

        /// <summary>
        ///     Execute a function using a connection, and transaction.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="function">The function to execute.</param>
        /// <returns>The result of the function.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<DbConnection, DbTransaction, Task<T>> function)
        {
            using (var connection = await GetConnectionAsync())
            {
                using (var transaction = connection.BeginTransaction(Configuration.TransactionIsolationLevel))
                {
                    return await function(connection, transaction);
                }
            }
        }

        /// <summary>
        ///     Execute a function using a connection, transaction, and command.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="function">The function to execute.</param>
        /// <returns>The result of the function.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<DbConnection, DbTransaction, DbCommand, Task<T>> function)
        {
            using (var connection = await GetConnectionAsync())
            {
                using (var transaction = connection.BeginTransaction(Configuration.TransactionIsolationLevel))
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.Transaction = transaction;
                        command.CommandTimeout = Configuration.DefaultCommandTimeoutSeconds;

                        return await function(connection, transaction, command);
                    }
                }
            }
        }

        /// <summary>
        ///     Get and open a connection to the database synchronously.
        /// </summary>
        /// <remarks>
        ///     Internally calls <see cref="GetConnectionAsync()" /> and waits for the result.
        /// </remarks>
        /// <returns>An <see cref="DbConnection" /> representing an open connection to the database.</returns>
        protected DbConnection GetConnection()
        {
            return GetConnectionAsync().Result;
        }

        /// <summary>
        ///     Get and open a connection to the database asynchronously.
        /// </summary>
        /// <returns>An <see cref="DbConnection" /> representing an open connection to the database.</returns>
        protected virtual async Task<DbConnection> GetConnectionAsync()
        {
            var connection = new SqlConnection(Configuration.RepositoryConnectionString);

            try
            {
                await connection.OpenAsync();
            }
            catch (Exception)
            {
                connection.Dispose();
                throw;
            }

            return connection;
        }

        /// <summary>
        ///     Get the query text for the calling method.
        ///     <see cref="QueryUtilities.GetEmbeddedQuery" /> and <see cref="QueryUtilities.Minify" /> for more details.
        ///     The resulting query text is cached by each repository on a per-class basis so each query is only extracted and
        ///     minified once.
        /// </summary>
        /// <param name="queryName">The name of the query to get. This defaults to the calling method.</param>
        /// <returns>The query text.</returns>
        protected string GetQuery([CallerMemberName] string queryName = null)
        {
            if (queryName == null)
                throw new ArgumentNullException(nameof(queryName));

            var type = GetType();
            var fullName = $"{type.FullName}.{queryName}";

            return Queries.GetOrAdd(fullName, x => GetQueryInternal(queryName, type));
        }

        private static string GetQueryInternal(string queryName, Type type)
        {
            // Get the query
            var query = QueryUtilities.GetEmbeddedQuery(queryName, type);

            // minify and return the query
            return QueryUtilities.Minify(query);
        }
    }
}