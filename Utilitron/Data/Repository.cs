using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Utilitron.Data
{
    /// <summary>
    ///     A base class for any Dapper based repositories.
    /// </summary>
    public abstract class Repository
    {
        private static readonly ConcurrentDictionary<string, string> Queries = new ConcurrentDictionary<string, string>();

        private static readonly ConcurrentDictionary<string, string> QueriesRaw = new ConcurrentDictionary<string, string>();

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
        /// <returns>The result of the function.</returns>
        protected async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, Task<T>> function)
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
        protected async Task<T> ExecuteAsync<T>(Func<IDbConnection, IDbTransaction, IDbCommand, Task<T>> function)
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
        /// <returns>An <see cref="IDbConnection" /> representing an open connection to the database.</returns>
        protected IDbConnection GetConnection()
        {
            return GetConnectionAsync()
                .Result;
        }

        /// <summary>
        ///     Create and open a connection to the database asynchronously.
        /// </summary>
        /// <returns>An <see cref="IDbConnection" /> representing an open connection to the database.</returns>
        protected abstract Task<IDbConnection> GetConnectionAsync();

        /// <summary>
        ///     Get the query text for the calling method.
        /// </summary>
        /// <remarks>
        ///     <see cref="QueryUtilities.GetEmbeddedQuery" /> and <see cref="QueryUtilities.Minify" /> for more details.
        ///     The resulting query text is cached by each repository on a per-class basis so each query is only extracted and
        ///     minified once.
        /// </remarks>
        /// <param name="queryName">The name of the query to get. This defaults to the calling method.</param>
        /// <returns>The query text (minified).</returns>
        protected string GetQuery([CallerMemberName] string queryName = null)
        {
            if (queryName == null)
                throw new ArgumentNullException(nameof(queryName));

            var type = GetType();

            var fullName = $"{type.FullName}.{queryName}()";

            return Queries.GetOrAdd(fullName, x =>
            {
                var q1 = QueriesRaw.GetOrAdd(fullName, y =>
                {
                    var q2 = QueryUtilities.GetEmbeddedQuery(queryName, type);

                    var preprocessorFlags = new Dictionary<string, bool>();

                    return QueryUtilities.Preprocess(preprocessorFlags, q2);
                });

                return QueryUtilities.Minify(q1);
            });
        }

        /// <summary>
        ///     Get the query text for the calling method.
        /// </summary>
        /// <remarks>
        ///     <see cref="QueryUtilities.GetEmbeddedQuery" />, <see cref="QueryUtilities.Preprocess"/> and <see cref="QueryUtilities.Minify" /> for more details.
        ///     The resulting query text is cached by each repository on a per-class basis so each query is only extracted and
        ///     minified once.
        /// </remarks>
        /// <param name="preprocessorFlags">A dictionary of named booleans necessary if using any preprocessor directives.</param>
        /// <param name="queryName">The name of the query to get. This defaults to the calling method.</param>
        /// <returns>The query text (minified).</returns>
        protected string GetQuery(IDictionary<string, bool> preprocessorFlags, [CallerMemberName] string queryName = null)
        {
            if (preprocessorFlags == null)
                throw new ArgumentNullException(nameof(queryName));

            if (queryName == null)
                throw new ArgumentNullException(nameof(queryName));

            var type = GetType();

            var flagsKey = GetPreProcessorIdentifier(preprocessorFlags);
            var fullName = $"{type.FullName}.{queryName}({flagsKey})";

            return Queries.GetOrAdd(fullName, x =>
            {
                var q1 = QueriesRaw.GetOrAdd(fullName, y =>
                {
                    var q2 = QueryUtilities.GetEmbeddedQuery(queryName, type);

                    return QueryUtilities.Preprocess(preprocessorFlags, q2);
                });

                return QueryUtilities.Minify(q1);
            });
        }

        /// <summary>
        ///     Get the query text for the calling method.
        ///     <see cref="QueryUtilities.GetEmbeddedQuery" /> for more details.
        ///     The resulting query text is cached by each repository on a per-class basis so each query is only extracted and
        ///     minified once.
        /// </summary>
        /// <param name="queryName">The name of the query to get. This defaults to the calling method.</param>
        /// <returns>The query text (unminified).</returns>
        protected string GetQueryRaw([CallerMemberName] string queryName = null)
        {
            if (queryName == null)
                throw new ArgumentNullException(nameof(queryName));

            var type = GetType();
            var fullName = $"{type.FullName}.{queryName}()";

            return QueriesRaw.GetOrAdd(fullName, x =>
            {
                var q = QueryUtilities.GetEmbeddedQuery(queryName, type);

                var preprocessorFlags = new Dictionary<string, bool>();

                return QueryUtilities.Preprocess(preprocessorFlags, q);
            });
        }

        /// <summary>
        ///     Get the query text for the calling method.
        ///     <see cref="QueryUtilities.GetEmbeddedQuery" /> and <see cref="QueryUtilities.Preprocess"/> for more details.
        ///     The resulting query text is cached by each repository on a per-class basis so each query is only extracted and
        ///     minified once.
        /// </summary>
        /// <param name="preprocessorFlags">A dictionary of named booleans necessary if using any preprocessor directives.</param>
        /// <param name="queryName">The name of the query to get. This defaults to the calling method.</param>
        /// <returns>The query text (unminified).</returns>
        protected string GetQueryRaw(IDictionary<string, bool> preprocessorFlags, [CallerMemberName] string queryName = null)
        {
            if (preprocessorFlags == null)
                throw new ArgumentNullException(nameof(queryName));

            if (queryName == null)
                throw new ArgumentNullException(nameof(queryName));

            var type = GetType();
            var flagsKey = GetPreProcessorIdentifier(preprocessorFlags);
            var fullName = $"{type.FullName}.{queryName}({flagsKey})";

            return QueriesRaw.GetOrAdd(fullName, x =>
            {
                var q = QueryUtilities.GetEmbeddedQuery(queryName, type);

                return QueryUtilities.Preprocess(preprocessorFlags, q);
            });
        }

        private static string GetPreProcessorIdentifier(IDictionary<string, bool> preprocessorFlags)
        {
            var strings = preprocessorFlags.OrderBy(x => x.Key)
                .Select(x => $"{x.Key}:{(x.Value ? "1" : "0")}");

            return string.Join(",", strings);
        }
    }
}
