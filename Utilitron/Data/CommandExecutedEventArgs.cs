using System;
using System.Data;

namespace Utilitron.Data
{
    /// <inheritdoc />
    public class CommandExecutedEventArgs : EventArgs
    {
        /// <summary>
        ///     The executed command.
        /// </summary>
        public readonly IDbCommand Command;

        /// <summary>
        ///     Create aninstance of <see cref="CommandExecutedEventArgs" />
        /// </summary>
        public CommandExecutedEventArgs(IDbCommand command)
        {
            Command = command;
        }
    }
}