using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Utilitron.Data.Profiling
{
    /// <summary>
    ///     A <see cref="DbConnection" /> that wraps another <see cref="DbConnection" /> and adds profiling events.
    /// </summary>
    public class ProfilingConnection: DbConnection
    {
        /// <summary>
        ///     The event fired when a <see cref="DbCommand" /> is successfully created by this <see cref="DbConnection" />.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbCommand>> CommandCreateEnd;

        /// <summary>
        ///     Event fired when a <see cref="DbCommand" /> finished ecxecuting a query either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbCommand>> CommandExecuteEnd;

        /// <summary>
        ///     Event fired when a <see cref="DbCommand" /> begins executing a query.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbCommand>> CommandExecuteStart;

        /// <summary>
        ///     The event fired when the <see cref="DbConnection" /> is finishes closing either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbConnection>> ConnectionCloseEnd;

        /// <summary>
        ///     The event fired when the <see cref="DbConnection" /> is begins to close.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbConnection>> ConnectionCloseStart;

        /// <summary>
        ///     The event fired when the <see cref="DbConnection" /> is finishes opening either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbConnection>> ConnectionOpenEnd;

        /// <summary>
        ///     The event fired when the <see cref="DbConnection" /> is begins to open.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbConnection>> ConnectionOpenStart;

        /// <summary>
        ///     The event fired when a <see cref="DbTransaction" /> is successfully created by this <see cref="DbConnection" />.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbTransaction>> TransactionBeginEnd;

        /// <summary>
        ///     The event fired when a <see cref="DbTransaction" /> finishes a commit either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbTransaction>> TransactionCommitEnd;

        /// <summary>
        ///     The event fired when a <see cref="DbTransaction" /> begins to commit.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbTransaction>> TransactionCommitStart;

        /// <summary>
        ///     The event fired when a <see cref="DbTransaction" /> finishes a roll back either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbTransaction>> TransactionRollbackEnd;

        /// <summary>
        ///     The event fired when a <see cref="DbTransaction" /> begins to roll back.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbTransaction>> TransactionRollbackStart;

        private ProfilingTransaction _currentTransaction;

        /// <inheritdoc />
        public override string ConnectionString
        {
            get => InnerConnection.ConnectionString;
            set => InnerConnection.ConnectionString = value;
        }

        /// <inheritdoc />
        public override int ConnectionTimeout => InnerConnection.ConnectionTimeout;

        /// <inheritdoc />
        public override string Database => InnerConnection.Database;

        /// <inheritdoc />
        public override string DataSource => InnerConnection.DataSource;

        internal DbConnection InnerConnection { get; }

        /// <inheritdoc />
        public override string ServerVersion => InnerConnection.ServerVersion;

        /// <inheritdoc />
        public override ConnectionState State => InnerConnection.State;

        /// <summary>
        ///     Create a new <see cref="ProfilingConnection" /> that wraps an existing <see cref="DbConnection" />.
        /// </summary>
        public ProfilingConnection(DbConnection connection)
        {
            if (connection == null)
                throw new ArgumentNullException(nameof(connection));

            InnerConnection = connection;
        }

        /// <inheritdoc />
        public override void ChangeDatabase(string databaseName)
        {
            InnerConnection.ChangeDatabase(databaseName);
        }

        /// <inheritdoc />
        public override void Close()
        {
            var startTime = DateTimeOffset.UtcNow;
            OnConnectionCloseStart(startTime);

            DateTimeOffset endTime;

            try
            {
                InnerConnection.Close();
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnConnectionCloseEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnConnectionCloseEnd(startTime, endTime);
        }

        /// <inheritdoc />
        public override void Open()
        {
            var startTime = DateTimeOffset.UtcNow;
            OnConnectionOpenStart(startTime);

            DateTimeOffset endTime;

            try
            {
                InnerConnection.Open();
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnConnectionOpenEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnConnectionOpenEnd(startTime, endTime);
        }

        /// <inheritdoc />
        public override async Task OpenAsync(CancellationToken cancellationToken)
        {
            var startTime = DateTimeOffset.UtcNow;
            OnConnectionOpenStart(startTime);

            DateTimeOffset endTime;

            try
            {
                await InnerConnection.OpenAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnConnectionOpenEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnConnectionOpenEnd(startTime, endTime);
        }

        /// <inheritdoc />
        protected override DbTransaction BeginDbTransaction(IsolationLevel il)
        {
            var startTime = DateTimeOffset.UtcNow;

            var transaction = InnerConnection.BeginTransaction(il);
            _currentTransaction = new ProfilingTransaction(this, transaction);
            ForwardTransactionEvents(_currentTransaction);

            var endTime = DateTimeOffset.UtcNow;
            OnTransactionBeginEnd(transaction, startTime, endTime);

            return _currentTransaction;
        }

        /// <inheritdoc />
        protected override DbCommand CreateDbCommand()
        {
            var startTime = DateTimeOffset.UtcNow;

            var command = InnerConnection.CreateCommand();
            var result = new ProfilingCommand(this, _currentTransaction, command);
            ForwardCommandEvents(result);

            var endTime = DateTimeOffset.UtcNow;
            OnCommandCreateEnd(command, startTime, endTime);

            return result;
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing)
                return;

            CommandCreateEnd.Deregister();
            CommandExecuteEnd.Deregister();
            CommandExecuteStart.Deregister();
            ConnectionCloseEnd.Deregister();
            ConnectionCloseStart.Deregister();
            ConnectionOpenEnd.Deregister();
            ConnectionOpenStart.Deregister();
            TransactionBeginEnd.Deregister();
            TransactionCommitEnd.Deregister();
            TransactionCommitStart.Deregister();
            TransactionRollbackEnd.Deregister();
            TransactionRollbackStart.Deregister();

            if (InnerConnection.State != ConnectionState.Closed)
                Close();

            InnerConnection.Dispose();
        }

        private void ForwardCommandEvents(ProfilingCommand command)
        {
            command.CommandExecuteEnd += (sender, args) => CommandExecuteEnd?.Invoke(this, args);
            command.CommandExecuteStart += (sender, args) => CommandExecuteStart?.Invoke(this, args);
        }

        private void ForwardTransactionEvents(ProfilingTransaction transaction)
        {
            transaction.TransactionCommitEnd += (sender, args) => TransactionCommitEnd?.Invoke(this, args);
            transaction.TransactionCommitStart += (sender, args) => TransactionCommitStart?.Invoke(this, args);
            transaction.TransactionRollbackEnd += (sender, args) => TransactionRollbackEnd?.Invoke(this, args);
            transaction.TransactionRollbackStart += (sender, args) => TransactionRollbackStart?.Invoke(this, args);
        }

        private void OnCommandCreateEnd(DbCommand command, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbCommand>(command, startTime, endTime);

            CommandCreateEnd?.Invoke(this, args);
        }

        private void OnConnectionCloseEnd(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbConnection>(InnerConnection, startTime, endTime);

            ConnectionCloseEnd?.Invoke(this, args);
        }

        private void OnConnectionCloseEnd(DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
        {
            var args = new ProfilingEventEndArgs<DbConnection>(InnerConnection, startTime, endTime, exception);

            ConnectionCloseEnd?.Invoke(this, args);
        }

        private void OnConnectionCloseStart(DateTimeOffset startTime)
        {
            var args = new ProfilingEventStartArgs<DbConnection>(InnerConnection, startTime);

            ConnectionCloseStart?.Invoke(this, args);
        }

        private void OnConnectionOpenEnd(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbConnection>(InnerConnection, startTime, endTime);

            ConnectionOpenEnd?.Invoke(this, args);
        }

        private void OnConnectionOpenEnd(DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
        {
            var args = new ProfilingEventEndArgs<DbConnection>(InnerConnection, startTime, endTime, exception);

            ConnectionOpenEnd?.Invoke(this, args);
        }

        private void OnConnectionOpenStart(DateTimeOffset startTime)
        {
            var args = new ProfilingEventStartArgs<DbConnection>(InnerConnection, startTime);

            ConnectionOpenStart?.Invoke(this, args);
        }

        private void OnTransactionBeginEnd(DbTransaction transaction, DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbTransaction>(transaction, startTime, endTime);

            TransactionBeginEnd?.Invoke(this, args);
        }
    }
}
