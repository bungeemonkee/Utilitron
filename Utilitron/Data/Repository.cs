using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utilitron.Data
{
    /// <summary>
    ///     A base class for any Dapper based repositories.
    /// </summary>
    public abstract class Repository
    {
        private static readonly ConcurrentDictionary<string, string> Queries = new ConcurrentDictionary<string, string>();

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
        /// <returns>The result of the fucntion.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<IDbConnection, Task<T>> function)
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
        /// <returns>The result of the fucntion.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> function)
        {
            using (var connection = await GetConnectionAsync())
            using (var transaction = connection.BeginTransaction(Configuration.TransactionIsolationLevel))
            {
                return await function(connection, transaction);
            }
        }

        /// <summary>
        ///     Execute a function using a connection, transaction, and command.
        /// </summary>
        /// <typeparam name="T">The return type.</typeparam>
        /// <param name="function">The function to execute.</param>
        /// <returns>The result of the fucntion.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, IDbCommand, Task<T>> function)
        {
            using (var connection = await GetConnectionAsync())
            using (var transaction = connection.BeginTransaction(Configuration.TransactionIsolationLevel))
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                command.CommandTimeout = Configuration.DefaultCommandTimeoutSeconds;

                return await function(connection, transaction, command);
            }
        }

        /// <summary>
        ///     Get and open a connection to the database synchronously.
        /// </summary>
        /// <remarks>
        ///     Internally calls <see cref="GetConnectionAsync()" /> and waits for the result.
        /// </remarks>
        /// <returns>An <see cref="IDbConnection" /> representing an open connection to the database.</returns>
        protected IDbConnection GetConnection()
        {
            return GetConnectionAsync().Result;
        }

        /// <summary>
        ///     Get and open a connection to the database asynchronously.
        /// </summary>
        /// <returns>An <see cref="IDbConnection" /> representing an open connection to the database.</returns>
        protected virtual async Task<IDbConnection> GetConnectionAsync()
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
        /// </summary>
        /// <param name="queryName">The name of the query to get. This defaults to the calling method.</param>
        /// <returns>The query text.</returns>
        protected string GetQuery([CallerMemberName] string queryName = null)
        {
            if (queryName == null)
            {
                throw new ArgumentNullException(nameof(queryName));
            }

            return Queries.GetOrAdd(queryName, GetQueryInternal);
        }

        private string GetQueryInternal(string queryName)
        {
            // Get the current type
            var type = GetType();

            // Get the member that matches this query
            var member = type.GetMethod(queryName);

            // Use the type that actually declared the member or the current type if it is not available
            type = member?.DeclaringType ?? type;

            // Add the type name (and 'Queries') to the query name
            queryName = $"{type.FullName}Queries.{queryName}.sql";

            // Get the embedded resource from the assembly
            var assembly = type.Assembly;
            using (var resource = assembly.GetManifestResourceStream(queryName))
            {
                if (resource == null)
                {
                    throw new InvalidOperationException($"No embedded query for {queryName}.");
                }

                using (var text = new StreamReader(resource, Encoding.UTF8, true))
                {
                    return text.ReadToEnd();
                }
            }
        }
    }
}