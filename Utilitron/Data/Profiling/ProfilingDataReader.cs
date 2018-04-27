using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Utilitron.Data.Profiling
{
    /// <summary>
    /// A <see cref="DbDataReader"/> that provides profiling events.
    /// </summary>
    public class ProfilingDataReader : DbDataReader
    {
        private readonly DateTimeOffset startTime;
        private bool isEnded;

        /// <summary>
        ///     Event fired when the <see cref="DbDataReader" /> hits the last row or is closed - whichever comes first.
        ///     This passed through to the command so that the actual time reading from the database can be tracked.
        /// </summary>
        public event EventHandler<ProfilingEventEndArgs<DbCommand>> CommandExecuteEnd;

        /// <inheritdoc />
        public override object this[int ordinal]
        {
            get
            {
                try
                {
                    return InternalReader[ordinal];
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override object this[string name]
        {
            get
            {
                try
                {
                    return InternalReader[name];
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override int Depth
        {
            get
            {
                try
                {
                    return InternalReader.Depth;
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override int FieldCount
        {
            get
            {
                try
                {
                    return InternalReader.FieldCount;
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override bool HasRows
        {
            get
            {
                try
                {
                    return InternalReader.HasRows;
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override bool IsClosed
        {
            get
            {
                try
                {
                    return InternalReader.IsClosed;
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override int RecordsAffected
        {
            get
            {
                try
                {
                    return InternalReader.RecordsAffected;
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        /// <inheritdoc />
        public override int VisibleFieldCount
        {
            get
            {
                try
                {
                    return InternalReader.VisibleFieldCount;
                }
                catch (Exception ex)
                {
                    var endTime = DateTimeOffset.Now;
                    OnCommandExecuteEnd(startTime, endTime, ex);

                    throw;
                }
            }
        }

        internal DbDataReader InternalReader { get; }

        /// <summary>
        /// Creates a new <see cref="ProfilingDataReader"/> that wraps the given <see cref="DbDataReader"/>.
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="startTime"></param>
        public ProfilingDataReader(DbDataReader inner, DateTimeOffset startTime)
        {
            InternalReader = inner;
            this.startTime = startTime;
        }

        /// <inheritdoc />
        public override void Close()
        {
            DateTimeOffset endTime;

            try
            {
                InternalReader.Close();
            }
            catch (Exception ex)
            {
                endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }

            endTime = DateTimeOffset.Now;
            OnCommandExecuteEnd(startTime, endTime);
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!disposing)
                return;

            DateTimeOffset endTime;

            try
            {
                InternalReader.Dispose();
            }
            catch (Exception ex)
            {
                endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }

            endTime = DateTimeOffset.Now;
            OnCommandExecuteEnd(startTime, endTime);
        }

        /// <inheritdoc />
        public override T GetFieldValue<T>(int ordinal)
        {
            try
            {
                return InternalReader.GetFieldValue<T>(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override async Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
        {
            try
            {
                return await InternalReader.GetFieldValueAsync<T>(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override Type GetProviderSpecificFieldType(int ordinal)
        {
            try
            {
                return InternalReader.GetProviderSpecificFieldType(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override object GetProviderSpecificValue(int ordinal)
        {
            try
            {
                return InternalReader.GetProviderSpecificValue(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override int GetProviderSpecificValues(object[] values)
        {
            try
            {
                return InternalReader.GetProviderSpecificValues(values);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override DataTable GetSchemaTable()
        {
            try
            {
                return InternalReader.GetSchemaTable();
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override Stream GetStream(int ordinal)
        {
            try
            {
                return InternalReader.GetStream(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override TextReader GetTextReader(int ordinal)
        {
            try
            {
                return InternalReader.GetTextReader(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override object InitializeLifetimeService()
        {
            try
            {
                return InternalReader.InitializeLifetimeService();
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override async Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
        {
            try
            {
                return await InternalReader.IsDBNullAsync(ordinal, cancellationToken);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override bool GetBoolean(int ordinal)
        {
            try
            {
                return InternalReader.GetBoolean(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override byte GetByte(int ordinal)
        {
            try
            {
                return InternalReader.GetByte(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            try
            {
                return InternalReader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override char GetChar(int ordinal)
        {
            try
            {
                return InternalReader.GetChar(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            try
            {
                return InternalReader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override string GetDataTypeName(int ordinal)
        {
            try
            {
                return InternalReader.GetDataTypeName(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override DateTime GetDateTime(int ordinal)
        {
            try
            {
                return InternalReader.GetDateTime(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override decimal GetDecimal(int ordinal)
        {
            try
            {
                return InternalReader.GetDecimal(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override double GetDouble(int ordinal)
        {
            try
            {
                return InternalReader.GetDouble(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override IEnumerator GetEnumerator()
        {
            IEnumerator result;
            try
            {
                result = InternalReader.GetEnumerator();
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }

            result = new ProfilingDataReaderEnumerator(result, OnCommandExecuteEnd);

            return result;
        }

        /// <inheritdoc />
        public override Type GetFieldType(int ordinal)
        {
            try
            {
                return InternalReader.GetFieldType(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override float GetFloat(int ordinal)
        {
            try
            {
                return InternalReader.GetFloat(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override Guid GetGuid(int ordinal)
        {
            try
            {
                return InternalReader.GetGuid(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override short GetInt16(int ordinal)
        {
            try
            {
                return InternalReader.GetInt16(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override int GetInt32(int ordinal)
        {
            try
            {
                return InternalReader.GetInt32(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override long GetInt64(int ordinal)
        {
            try
            {
                return InternalReader.GetInt64(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override string GetName(int ordinal)
        {
            try
            {
                return InternalReader.GetName(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override int GetOrdinal(string name)
        {
            try
            {
                return InternalReader.GetOrdinal(name);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override string GetString(int ordinal)
        {
            try
            {
                return InternalReader.GetString(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override object GetValue(int ordinal)
        {
            try
            {
                return InternalReader.GetValue(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override int GetValues(object[] values)
        {
            try
            {
                return InternalReader.GetValues(values);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override bool IsDBNull(int ordinal)
        {
            try
            {
                return InternalReader.IsDBNull(ordinal);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override bool NextResult()
        {
            bool result;

            try
            {
                result = InternalReader.NextResult();
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }

            if (!result)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime);
            }

            return result;
        }

        /// <inheritdoc />
        public override async Task<bool> NextResultAsync(CancellationToken cancellationToken)
        {
            bool result;

            try
            {
                result = await InternalReader.NextResultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }

            if (!result)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime);
            }

            return result;
        }

        /// <inheritdoc />
        public override bool Read()
        {
            try
            {
                return InternalReader.Read();
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        /// <inheritdoc />
        public override async Task<bool> ReadAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await InternalReader.ReadAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                var endTime = DateTimeOffset.Now;
                OnCommandExecuteEnd(startTime, endTime, ex);

                throw;
            }
        }

        private void OnCommandExecuteEnd(Exception exception)
        {
            if (isEnded)
            {
                // Do not fire this event twice
                return;
            }

            isEnded = true;

            var endTime = DateTimeOffset.Now;
            var args = new ProfilingEventEndArgs<DbCommand>(null, startTime, endTime, exception);

            CommandExecuteEnd?.Invoke(this, args);
        }

        private void OnCommandExecuteEnd(DateTimeOffset startTime, DateTimeOffset endTime)
        {
            if (isEnded)
            {
                // Do not fire this event twice
                return;
            }

            isEnded = true;

            var args = new ProfilingEventEndArgs<DbCommand>(null, startTime, endTime);

            CommandExecuteEnd?.Invoke(this, args);
        }

        private void OnCommandExecuteEnd(DateTimeOffset startTime, DateTimeOffset endTime, Exception exception)
        {
            if (isEnded)
            {
                // Do not fire this event twice
                return;
            }

            isEnded = true;

            var args = new ProfilingEventEndArgs<DbCommand>(null, startTime, endTime, exception);

            CommandExecuteEnd?.Invoke(this, args);
        }
    }
}
