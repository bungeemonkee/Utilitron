using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Utilitron.Data.Profiling
{
    /// <summary>
    ///     A <see cref="DbCommand" /> that wraps another <see cref="DbTransaction" /> and adds profiling events.
    /// </summary>
    public class ProfilingCommand: DbCommand
    {
        /// <summary>
        ///     Event fired when the <see cref="DbCommand" /> finished executing a query either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbCommand>> CommandExecuteEnd;

        /// <summary>
        ///     Event fired when the <see cref="DbCommand" /> begins executing a query.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbCommand>> CommandExecuteStart;

        private readonly DbCommand _command;

        private ProfilingConnection _connection;
        private ProfilingTransaction _transaction;

        /// <inheritdoc />
        public override string CommandText
        {
            get => _command.CommandText;
            set => _command.CommandText = value;
        }

        /// <inheritdoc />
        public override int CommandTimeout
        {
            get => _command.CommandTimeout;
            set => _command.CommandTimeout = value;
        }

        /// <inheritdoc />
        public override CommandType CommandType
        {
            get => _command.CommandType;
            set => _command.CommandType = value;
        }

        /// <inheritdoc />
        protected override DbConnection DbConnection
        {
            get => _connection;
            set
            {
                switch (value)
                {
                    case null:
                        _command.Connection = null;
                        _connection = null;
                        break;
                    case ProfilingConnection profilingConnection:
                        _command.Connection = profilingConnection.InnerConnection;
                        _connection = profilingConnection;
                        break;
                    default:
                        _command.Connection = value;
                        _connection = new ProfilingConnection(value);
                        break;
                }
            }
        }

        /// <inheritdoc />
        protected override DbParameterCollection DbParameterCollection => _command.Parameters;

        /// <inheritdoc />
        protected override DbTransaction DbTransaction
        {
            get => _transaction;
            set
            {
                switch (value)
                {
                    case null:
                        _command.Transaction = null;
                        _transaction = null;
                        break;
                    case ProfilingTransaction profilingTransaction:
                        _command.Transaction = profilingTransaction.InnerTransaction;
                        _transaction = profilingTransaction;
                        break;
                    default:
                        _command.Transaction = value;
                        _transaction = new ProfilingTransaction(value);
                        break;
                }
            }
        }

        /// <inheritdoc />
        public override bool DesignTimeVisible
        {
            get => _command.DesignTimeVisible;
            set => _command.DesignTimeVisible = value;
        }

        /// <inheritdoc />
        public override UpdateRowSource UpdatedRowSource
        {
            get => _command.UpdatedRowSource;
            set => _command.UpdatedRowSource = value;
        }

        /// <summary>
        ///     Create a new <see cref="ProfilingCommand" /> that wraps a given <see cref="DbCommand" />.
        /// </summary>
        public ProfilingCommand(DbCommand command)
            : this(command.Connection, command.Transaction, command)
        {
        }

        internal ProfilingCommand(DbConnection connection, DbTransaction transaction, DbCommand command)
        {
            _connection = connection == null
                ? null
                : connection as ProfilingConnection ?? new ProfilingConnection(connection);

            _transaction = transaction == null
                ? null
                : transaction as ProfilingTransaction ?? new ProfilingTransaction(_connection, transaction);

            _command = command;
        }

        /// <inheritdoc />
        public override void Cancel()
        {
            _command.Cancel();
        }

        /// <inheritdoc />
        public override int ExecuteNonQuery()
        {
            var startTime = DateTimeOffset.UtcNow;
            OnCommandExecuteStart(startTime);

            int result;
            DateTimeOffset endTime;

            try
            {
                result = _command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnCommandExecuteEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnCommandExecuteEnd(startTime, endTime);

            return result;
        }

        /// <inheritdoc />
        public override async Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)
        {
            var startTime = DateTimeOffset.UtcNow;
            OnCommandExecuteStart(startTime);

            int result;
            DateTimeOffset endTime;

            try
            {
                result = await _command.ExecuteNonQueryAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnCommandExecuteEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnCommandExecuteEnd(startTime, endTime);

            return result;
        }

        /// <inheritdoc />
        public override object ExecuteScalar()
        {
            var startTime = DateTimeOffset.UtcNow;
            OnCommandExecuteStart(startTime);

            object result;
            DateTimeOffset endTime;

            try
            {
                result = _command.ExecuteScalar();
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnCommandExecuteEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnCommandExecuteEnd(startTime, endTime);

            return result;
        }

        /// <inheritdoc />
        public override async Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)
        {
            var startTime = DateTimeOffset.UtcNow;
            OnCommandExecuteStart(startTime);

            object result;
            DateTimeOffset endTime;

            try
            {
                result = await _command.ExecuteScalarAsync(cancellationToken);
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnCommandExecuteEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnCommandExecuteEnd(startTime, endTime);

            return result;
        }

        /// <inheritdoc />
        public override void Prepare()
        {
            _command.Prepare();
        }

        /// <inheritdoc />
        protected override DbParameter CreateDbParameter()
        {
            return _command.CreateParameter();
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
                _command.Dispose();
        }

        /// <inheritdoc />
        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            var startTime = DateTimeOffset.UtcNow;
            OnCommandExecuteStart(startTime);

            DbDataReader result;
            DateTimeOffset endTime;

            try
            {
                result = _command.ExecuteReader(behavior);
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnCommandExecuteEnd(startTime, endTime, exception);

                throw;
            }

            // Allow the reader to handle the command end event
            result = new ProfilingDataReader(result, startTime);
            ((ProfilingDataReader)result).CommandExecuteEnd += OnCommandExecuteEnd;

            return result;
        }

        /// <inheritdoc />
        protected override async Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
        {
            var startTime = DateTimeOffset.UtcNow;
            OnCommandExecuteStart(startTime);

            DbDataReader result;
            DateTimeOffset endTime;

            try
            {
                result = await _command.ExecuteReaderAsync(behavior, cancellationToken);
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnCommandExecuteEnd(startTime, endTime, exception);

                throw;
            }

            // Allow the reader to handle the command end event
            result = new ProfilingDataReader(result, startTime);
            ((ProfilingDataReader)result).CommandExecuteEnd += OnCommandExecuteEnd;

            return result;
        }

        private void OnCommandExecuteEnd(object sender, ProfilingEventEndArgs<DbCommand> args)
        {
            args = new ProfilingEventEndArgs<DbCommand>(_command, args.StartTime, args.EndTime, args.Exception);

            CommandExecuteEnd?.Invoke(this, args);
        }

        private void OnCommandExecuteEnd(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbCommand>(_command, startTime, endTime);

            CommandExecuteEnd?.Invoke(this, args);
        }

        private void OnCommandExecuteEnd(DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
        {
            var args = new ProfilingEventEndArgs<DbCommand>(_command, startTime, endTime, exception);

            CommandExecuteEnd?.Invoke(this, args);
        }

        private void OnCommandExecuteStart(DateTimeOffset startTime)
        {
            var args = new ProfilingEventStartArgs<DbCommand>(_command, startTime);

            CommandExecuteStart?.Invoke(this, args);
        }
    }
}
