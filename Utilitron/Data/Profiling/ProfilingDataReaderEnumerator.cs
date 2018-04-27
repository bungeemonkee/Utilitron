using System;
using System.Collections;

namespace Utilitron.Data.Profiling
{
    /// <summary>
    /// A <see cref="IEnumerator"/> that wraps <see cref="IEnumerator"/>s from a <see cref="ProfilingDataReader"/>.
    /// </summary>
    public class ProfilingDataReaderEnumerator : IEnumerator
    {
        private readonly Action<Exception> callback;
        internal readonly IEnumerator InternalEnumerator;

        /// <inheritdoc />
        public object Current
        {
            get
            {
                try
                {
                    return InternalEnumerator.Current;
                }
                catch (Exception ex)
                {
                    callback(ex);

                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inner"></param>
        /// <param name="callback"></param>
        public ProfilingDataReaderEnumerator(IEnumerator inner, Action<Exception> callback)
        {
            InternalEnumerator = inner;
            this.callback = callback;
        }

        /// <inheritdoc />
        public bool MoveNext()
        {
            bool result;
            try
            {
                result = InternalEnumerator.MoveNext();
            }
            catch (Exception ex)
            {
                callback(ex);

                throw;
            }

            if (!result)
            {
                callback(null);
            }

            return result;
        }

        /// <inheritdoc />
        public void Reset()
        {
            try
            {
                InternalEnumerator.Reset();
            }
            catch (Exception ex)
            {
                callback(ex);

                throw;
            }
        }
    }
}
