using System;
using System.Data;
using System.Data.Common;

namespace Utilitron.Data.Profiling
{
    /// <summary>
    ///     A <see cref="DbTransaction" /> that wraps another <see cref="DbTransaction" /> and adds profiling events.
    /// </summary>
    public class ProfilingTransaction: DbTransaction
    {
        /// <summary>
        ///     The event fired when the <see cref="DbTransaction" /> finishes a commit either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbTransaction>> TransactionCommitEnd;

        /// <summary>
        ///     The event fired when the <see cref="DbTransaction" /> begins to commit.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbTransaction>> TransactionCommitStart;

        /// <summary>
        ///     The event fired when the <see cref="DbTransaction" /> finishes a roll back either successfully or due to failure.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbTransaction>> TransactionRollbackEnd;

        /// <summary>
        ///     The event fired when the <see cref="DbTransaction" /> begins to roll back.
        /// </summary>
        public event EventHandler<ProfilingEventStartArgs<DbTransaction>> TransactionRollbackStart;

        private readonly ProfilingConnection _connection;

        internal readonly DbTransaction InnerTransaction;

        /// <inheritdoc />
        protected override DbConnection DbConnection => _connection;

        /// <inheritdoc />
        public override IsolationLevel IsolationLevel => InnerTransaction.IsolationLevel;

        /// <summary>
        ///     Create a new <see cref="ProfilingTransaction" /> that wraps an existing <see cref="DbTransaction" />.
        /// </summary>
        public ProfilingTransaction(DbTransaction transaction)
            : this(transaction.Connection, transaction)
        {
        }

        internal ProfilingTransaction(DbConnection connection, DbTransaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            _connection = connection != null
                ? connection as ProfilingConnection ?? new ProfilingConnection(connection)
                : transaction.Connection as ProfilingConnection ?? new ProfilingConnection(transaction.Connection);

            InnerTransaction = transaction;
        }

        /// <inheritdoc />
        public override void Commit()
        {
            var startTime = DateTimeOffset.UtcNow;
            OnTransactionCommitStart(startTime);

            DateTimeOffset endTime;

            try
            {
                InnerTransaction.Commit();
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnTransactionCommitEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnTransactionCommitEnd(startTime, endTime);
        }

        /// <inheritdoc />
        public override void Rollback()
        {
            var startTime = DateTimeOffset.UtcNow;
            OnTransactionRollbackStart(startTime);

            DateTimeOffset endTime;

            try
            {
                InnerTransaction.Rollback();
            }
            catch (Exception exception)
            {
                endTime = DateTimeOffset.UtcNow;
                OnTransactionRollbackEnd(startTime, endTime, exception);

                throw;
            }

            endTime = DateTimeOffset.UtcNow;
            OnTransactionRollbackEnd(startTime, endTime);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing)
                return;

            TransactionCommitEnd.Deregister();
            TransactionCommitStart.Deregister();
            TransactionRollbackEnd.Deregister();
            TransactionRollbackStart.Deregister();

            InnerTransaction.Dispose();
        }

        private void OnTransactionCommitEnd(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbTransaction>(InnerTransaction, startTime, endTime);

            TransactionCommitEnd?.Invoke(this, args);
        }

        private void OnTransactionCommitEnd(DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
        {
            var args = new ProfilingEventEndArgs<DbTransaction>(InnerTransaction, startTime, endTime, exception);

            TransactionCommitEnd?.Invoke(this, args);
        }

        private void OnTransactionCommitStart(DateTimeOffset startTime)
        {
            var args = new ProfilingEventStartArgs<DbTransaction>(InnerTransaction, startTime);

            TransactionCommitStart?.Invoke(this, args);
        }

        private void OnTransactionRollbackEnd(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            var args = new ProfilingEventEndArgs<DbTransaction>(InnerTransaction, startTime, endTime);

            TransactionRollbackEnd?.Invoke(this, args);
        }

        private void OnTransactionRollbackEnd(DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
        {
            var args = new ProfilingEventEndArgs<DbTransaction>(InnerTransaction, startTime, endTime, exception);

            TransactionRollbackEnd?.Invoke(this, args);
        }

        private void OnTransactionRollbackStart(DateTimeOffset startTime)
        {
            var args = new ProfilingEventStartArgs<DbTransaction>(InnerTransaction, startTime);

            TransactionRollbackStart?.Invoke(this, args);
        }
    }
}
