using System;
using System.Data;

namespace Utilitron.Data
{
    /// <inheritdoc />
    public class CommandFailedEventArgs : EventArgs
    {
        /// <summary>
        ///     The executed command.
        /// </summary>
        public readonly IDbCommand Command;

        /// <summary>
        ///     The exception that caused the command to fail.
        /// </summary>
        public readonly Exception Exception;

        /// <summary>
        ///     Create aninstance of <see cref="CommandFailedEventArgs" />
        /// </summary>
        public CommandFailedEventArgs(IDbCommand command, Exception exception)
        {
            Command = command;
            Exception = exception;
        }
    }
}